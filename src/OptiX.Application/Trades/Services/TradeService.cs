using OptiX.Application.Ticks.Services;
using OptiX.Application.Trades.Mappers;
using OptiX.Application.Trades.Requests;
using OptiX.Application.Trades.Responses;
using OptiX.Domain.Entities.Trading;
using Optix.Infrastructure.Database;

namespace OptiX.Application.Trades.Services;

public sealed class TradeService : ITradeService
{
    private readonly AppDbContext _context;
    private readonly ITickService _tickService;

    public TradeService(AppDbContext context, ITickService tickService)
    {
        _context = context;
        _tickService = tickService;
    }

    public async Task<TradeDto?> OpenTradeAsync(OpenTradeRequest request)
    {
        var lastTick = await _tickService.GetLastTickAsync(request.AssetId);
        if (lastTick == null)
            return null;
        
        var trade = new Trade(request.AccountId, request.AssetId, request.Direction, request.DurationMinutes, request.Amount, lastTick.Price);
        await _context.Trades.AddAsync(trade);
        await _context.SaveChangesAsync();

        return trade.ToDto();
    }

    public async Task<TradeDto?> CloseTradeAsync(CloseTradeRequest request)
    {
        var trade = await _context.Trades.FindAsync(request.TradeId);
        if (trade is null)
            return null;
        
        var lastTick = await _tickService.GetLastTickAsync(trade.AssetId);
        if (lastTick == null)
            return null;
        
        trade.Close(lastTick.Price);
        await _context.SaveChangesAsync();

        return trade.ToDto();
    }

    public async Task<TradeDto?> GetAsync(Guid id)
    {
        var trade = await _context.Trades.FindAsync(id);
        return trade?.ToDto();
    }
}