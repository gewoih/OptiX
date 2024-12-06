namespace OptiX.Application.MarketData.Services;

public interface IMarketDataService
{
    Task SaveAsync(IEnumerable<TickDto> ticks);
    Task<TickDto?> GetLastTickAsync(Guid assetId);
}