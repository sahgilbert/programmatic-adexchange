namespace ProgrammaticAdExchange.Models;

public class BidResponse
{
    public string RequestId { get; set; }
    public Bid WinningBid { get; set; }
}

public class Bid
{
    public string Bidder { get; set; }
    public string ImpId { get; set; }
    public decimal Price { get; set; }
}