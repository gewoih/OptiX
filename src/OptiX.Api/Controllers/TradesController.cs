using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OptiX.Application.Trades.Requests;
using OptiX.Application.Trades.Services;

namespace OptiX.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TradesController : ControllerBase
{
    private readonly ITradeService _tradeService;

    public TradesController(ITradeService tradeService)
    {
        _tradeService = tradeService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OpenTradeRequest request)
    {
        var openedTrade = await _tradeService.OpenTradeAsync(request);
        if (openedTrade == null)
            return BadRequest();
        
        return CreatedAtAction(nameof(Get), new { id = openedTrade.Id }, openedTrade);
    }

    [HttpPatch]
    public async Task<IActionResult> Patch([FromBody] CloseTradeRequest request)
    {
        var trade = await _tradeService.CloseTradeAsync(request);
        if (trade is null)
            return NotFound();
        
        return Ok(trade);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var trade = await _tradeService.GetAsync(id);
        if (trade is null)
            return NotFound();

        return Ok(trade);
    }
}