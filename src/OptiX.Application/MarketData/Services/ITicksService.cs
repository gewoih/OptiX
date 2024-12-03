namespace OptiX.Application.MarketData.Services;

public interface ITicksService
{
    Task SaveAsync(Guid assetId, IEnumerable<TickDto> ticks);
}