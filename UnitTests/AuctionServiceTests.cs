namespace UnitTests;

public class AuctionServiceTests
{
    private const string BID_REQUEST_ID = "req-123";

    private readonly IAuctionService _auctionService;

    public AuctionServiceTests()
    {
        _auctionService = new AuctionService();
    }

    [Fact]
    public void ReturnsNull_WhenNoValidBids()
    {
        // Arrange
        var imp = new Impression { Id = "1", BidFloor = 0.50m };
        var bids = new List<Bid>
        {
            new Bid { Bidder = "DSP-A", ImpId = "1", Price = 0.30m }, // Below floor
            new Bid { Bidder = "DSP-B", ImpId = "2", Price = 1.00m }  // Wrong impression
        };

        // Act
        var result = _auctionService.RunSecondPriceAuction(BID_REQUEST_ID, imp, bids);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ReturnsSingleBid_AtBidFloor_WhenOnlyOneBid()
    {
        // Arrange
        var imp = new Impression { Id = "1", BidFloor = 0.50m };
        var bids = new List<Bid>
        {
            new Bid { Bidder = "DSP-A", ImpId = "1", Price = 1.00m }
        };

        // Act
        var result = _auctionService.RunSecondPriceAuction(BID_REQUEST_ID, imp, bids);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.WinningBid);
        Assert.Equal("DSP-A", result.WinningBid.Bidder);
        Assert.Equal(0.50m, result.WinningBid.Price); // Pays bid floor (second price fallback)
    }

    [Fact]
    public void ReturnsSecondPrice_WhenMultipleValidBids()
    {
        // Arrange
        var imp = new Impression { Id = "1", BidFloor = 0.50m };
        var bids = new List<Bid>
        {
            new Bid { Bidder = "DSP-A", ImpId = "1", Price = 1.20m },
            new Bid { Bidder = "DSP-B", ImpId = "1", Price = 1.00m },
            new Bid { Bidder = "DSP-C", ImpId = "1", Price = 0.80m }
        };

        // Act
        var result = _auctionService.RunSecondPriceAuction(BID_REQUEST_ID, imp, bids);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.WinningBid);
        Assert.Equal("DSP-A", result.WinningBid.Bidder);
        Assert.Equal(1.00m, result.WinningBid.Price); // Second-highest bid
    }

    [Fact]
    public void IgnoresBidsBelowBidFloor()
    {
        // Arrange
        var imp = new Impression { Id = "1", BidFloor = 0.50m };
        var bids = new List<Bid>
        {
            new Bid { Bidder = "DSP-A", ImpId = "1", Price = 0.40m }, // Ignored
            new Bid { Bidder = "DSP-B", ImpId = "1", Price = 0.60m },
            new Bid { Bidder = "DSP-C", ImpId = "1", Price = 0.70m }
        };

        // Act
        var result = _auctionService.RunSecondPriceAuction(BID_REQUEST_ID, imp, bids);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.WinningBid);
        Assert.Equal("DSP-C", result.WinningBid.Bidder);
        Assert.Equal(0.60m, result.WinningBid.Price);
    }
}