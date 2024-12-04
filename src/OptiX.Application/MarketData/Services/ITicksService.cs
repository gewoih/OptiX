namespace OptiX.Application.MarketData.Services;

public interface ITicksService
{
    Task SaveAsync(IEnumerable<TickDto> ticks);
}