using OptiX.Application.Trades.Requests;
using OptiX.Application.Trades.Responses;

namespace OptiX.Application.Trades.Services;

public interface ITradeService
{
    Task<TradeDto?> OpenTradeAsync(OpenTradeRequest request);
    Task<TradeDto?> CloseTradeAsync(CloseTradeRequest request);
    Task<TradeDto?> GetAsync(Guid id);
}