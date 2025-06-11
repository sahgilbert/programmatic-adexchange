namespace ProgrammaticAdExchange.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuctionController : ControllerBase
{
    private readonly IAuctionService _auctionService;

    public AuctionController(IAuctionService auctionService)
    {
        _auctionService = auctionService;
    }

    [HttpPost]
    public IActionResult SubmitBidRequest([FromBody] BidRequest bidRequest)
    {
        if (bidRequest == null || bidRequest.Imp == null || !bidRequest.Imp.Any())
            return BadRequest("Invalid bid request");

        var impression = bidRequest.Imp.First();

        var simulatedBids = SimulationService.SimulateDspBids(impression);

        var bidResponse = _auctionService.RunSecondPriceAuction(
            bidRequest.Id,
            impression,
            simulatedBids);
        
        return Ok(bidResponse);
    }
}