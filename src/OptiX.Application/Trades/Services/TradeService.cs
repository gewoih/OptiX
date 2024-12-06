using MassTransit;
using OptiX.Application.MarketData.Services;
using OptiX.Application.Trades.Mappers;
using OptiX.Application.Trades.Requests;
using OptiX.Application.Trades.Responses;
using OptiX.Application.Transactions.Requests;
using OptiX.Application.Transactions.Services;
using OptiX.Domain.Entities.Trading;
using OptiX.Domain.ValueObjects;
using Optix.Infrastructure.Database;

namespace OptiX.Application.Trades.Services;

public sealed class TradeService : ITradeService
{
    private readonly AppDbContext _context;
    private readonly IMarketDataService _marketDataService;
    private readonly ITransactionService _transactionService;
    private readonly IMessageScheduler _messageScheduler;

    public TradeService(AppDbContext context, IMarketDataService marketDataService, ITransactionService transactionService, IMessageScheduler messageScheduler)
    {
        _context = context;
        _marketDataService = marketDataService;
        _transactionService = transactionService;
        _messageScheduler = messageScheduler;
    }

    public async Task<TradeDto?> OpenTradeAsync(OpenTradeRequest request)
    {
        var lastTick = await _marketDataService.GetLastTickAsync(request.AssetId);
        if (lastTick == null)
            return null;

        await using var transaction = await _context.Database.BeginTransactionAsync();

        var trade = new Trade(request.AccountId, request.AssetId, request.Direction, request.DurationMinutes,
            request.Amount, lastTick.Price);
        var tradeTransaction =
            new CreateTransactionRequest(request.AccountId, TransactionTrigger.TradeOpening, -trade.OpenSum);
        
        try
        {
            await _transactionService.CreateTransactionAsync(tradeTransaction);
            
            await _context.Trades.AddAsync(trade);
            await _messageScheduler.SchedulePublish(trade.PlannedClosingDate, new CloseTradeRequest(trade.Id));
            
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            
            return trade.ToDto();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<TradeDto?> CloseTradeAsync(CloseTradeRequest request)
    {
        var trade = await _context.Trades.FindAsync(request.TradeId);
        if (trade is null)
            return null;

        var lastTick = await _marketDataService.GetLastTickAsync(trade.AssetId);
        if (lastTick == null)
            return null;

        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            trade.Close(lastTick.Price);
            
            var tradeTransactionRequest = new CreateTransactionRequest(trade.AccountId, TransactionTrigger.TradeClosing, trade.CloseSum);
            var commissionTransactionRequest = new CreateTransactionRequest(trade.AccountId, TransactionTrigger.Commission, -trade.Commission);
            await _transactionService.CreateTransactionAsync(tradeTransactionRequest);
            await _transactionService.CreateTransactionAsync(commissionTransactionRequest);
            
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw;
        }

        return trade.ToDto();
    }

    public async Task<TradeDto?> GetAsync(Guid id)
    {
        var trade = await _context.Trades.FindAsync(id);
        return trade?.ToDto();
    }
}