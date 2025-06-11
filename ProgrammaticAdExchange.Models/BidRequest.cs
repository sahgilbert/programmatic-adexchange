namespace ProgrammaticAdExchange.Models;

public class BidRequest
{
    public string Id { get; set; }  // Unique request ID
    public List<Impression> Imp { get; set; }
}

public class Impression
{
    public string Id { get; set; }
    public decimal BidFloor { get; set; }
}