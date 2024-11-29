using Optix.Infrastructure.Database;

namespace OptiX.Application.MarketData;

public sealed class TicksService : ITicksService
{
    private readonly AppDbContext _context;

    public TicksService(AppDbContext context)
    {
        _context = context;
    }

    public async Task SaveAsync(Guid assetId, IEnumerable<TickDto> ticks)
    {
        var marketDataToSave = ticks.Select(marketData => new Domain.Entities.Asset.Tick
        {
            AssetId = assetId,
            Price = marketData.Price,
            Volume = marketData.Volume,
            DateTime = marketData.DateTime
        });
        
        await _context.Ticks.AddRangeAsync(marketDataToSave);
        await _context.SaveChangesAsync();
    }
}