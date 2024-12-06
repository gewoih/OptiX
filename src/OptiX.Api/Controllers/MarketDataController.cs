using Microsoft.AspNetCore.Mvc;
using OptiX.Application.MarketData.Requests;
using OptiX.Application.MarketData.Services;

namespace OptiX.Api.Controllers;

[Route("api/market-data")]
[ApiController]
public class MarketDataController : ControllerBase
{
    private readonly IMarketDataService _marketDataService;

    public MarketDataController(IMarketDataService marketDataService)
    {
        _marketDataService = marketDataService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetMarketDataRequest request)
    {
        var priceCandles = await _marketDataService.GetPriceCandlesAsync(request);
        return Ok(priceCandles);
    }
}