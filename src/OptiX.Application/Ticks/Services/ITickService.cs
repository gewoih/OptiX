namespace OptiX.Application.Ticks.Services;

public interface ITickService
{
    Task SaveAsync(IEnumerable<TickDto> ticks);
    Task<TickDto?> GetLastTickAsync(Guid assetId);
}