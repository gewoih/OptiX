using OptiX.Application.MarketData.Requests;
using OptiX.Application.MarketData.Responses;

namespace OptiX.Application.MarketData.Services;

public interface IMarketDataService
{
    Task SaveAsync(IEnumerable<TickDto> ticks);
    Task<TickDto?> GetLastTickAsync(string symbol);
    Task<List<PriceCandleDto>> GetPriceCandlesAsync(GetMarketDataRequest request);
}