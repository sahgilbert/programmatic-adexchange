namespace ProgrammaticAdExchange.Services;

public sealed class AuctionService : IAuctionService
{
    public BidResponse RunSecondPriceAuction(string bidRequestId, Impression impression, List<Bid> bids)
    {
        var validBids = bids
            .Where(b => b.ImpId == impression.Id && b.Price >= impression.BidFloor)
            .OrderByDescending(b => b.Price)
            .ToList();

        if (!validBids.Any())
        {
            return null;
        }

        var winningBid = validBids[0];

        const int SECOND_PLACE_ID = 1;

        var secondPrice = validBids.Count > 1 ? validBids[SECOND_PLACE_ID].Price : impression.BidFloor;

        if (winningBid == null)
        {
            return new BidResponse
            {
                RequestId = bidRequestId,
                WinningBid = null
            };
        }
        else
        {
            var winningBidResult = new Bid
            {
                Bidder = winningBid.Bidder,
                ImpId = winningBid.ImpId,
                Price = secondPrice
            };

            return new BidResponse
            {
                RequestId = bidRequestId,
                WinningBid = winningBidResult
            };
        }
    }
}