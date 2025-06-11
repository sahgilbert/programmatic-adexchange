namespace ProgrammaticAdExchange.Services;

public static class SimulationService
{
    public static List<Bid> SimulateDspBids(Impression impression)
    {
        return new List<Bid>
        {
            new Bid { Bidder = "DSP-A", ImpId = impression.Id, Price = 1.10m },
            new Bid { Bidder = "DSP-B", ImpId = impression.Id, Price = 0.90m },
            new Bid { Bidder = "DSP-C", ImpId = impression.Id, Price = 0.40m }
        };
    }
}