using OptiX.Application.Trades.Mappers;
using OptiX.Application.Trades.Requests;
using OptiX.Application.Trades.Responses;
using OptiX.Domain.Entities.Trading;
using Optix.Infrastructure.Database;

namespace OptiX.Application.Trades.Services;

public sealed class TradeService : ITradeService
{
    private readonly AppDbContext _context;

    public TradeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TradeDto> OpenTradeAsync(OpenTradeRequest request)
    {
        var trade = new Trade(request.AccountId, request.Direction, request.DurationMinutes, request.Amount, 0);
        await _context.Trades.AddAsync(trade);
        await _context.SaveChangesAsync();

        return trade.ToDto();
    }

    public async Task<TradeDto> CloseTradeAsync(CloseTradeRequest request)
    {
        var existingTrade = await _context.Trades.FindAsync(request.TradeId);
        if (existingTrade is null)
            throw new Exception();

        existingTrade.Close(0);
        await _context.SaveChangesAsync();

        return existingTrade.ToDto();
    }

    public async Task<TradeDto?> GetAsync(Guid id)
    {
        var trade = await _context.Trades.FindAsync(id);
        return trade is null ? null : trade.ToDto();
    }
}