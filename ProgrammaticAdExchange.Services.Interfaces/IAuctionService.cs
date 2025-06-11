namespace ProgrammaticAdExchange.Services.Interfaces;

public interface IAuctionService
{
    BidResponse RunSecondPriceAuction(string bidRequestId, Impression impression, List<Bid> bids);
}