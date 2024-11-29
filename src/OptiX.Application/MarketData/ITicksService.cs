namespace OptiX.Application.MarketData;

public interface ITicksService
{
    Task SaveAsync(Guid assetId, IEnumerable<TickDto> ticks);
}