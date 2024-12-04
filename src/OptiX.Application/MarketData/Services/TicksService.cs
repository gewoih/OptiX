using Optix.Infrastructure.Database;

namespace OptiX.Application.MarketData.Services;

public sealed class TicksService : ITicksService
{
    private readonly AppDbContext _context;

    public TicksService(AppDbContext context)
    {
        _context = context;
    }

    public async Task SaveAsync(IEnumerable<TickDto> ticks)
    {
        var marketDataToSave = ticks.Select(marketData => new Domain.Entities.Asset.Tick
        {
            AssetId = marketData.AssetId,
            Price = marketData.Price,
            Volume = marketData.Volume,
            Date = marketData.DateTime
        });
        
        await _context.Ticks.AddRangeAsync(marketDataToSave);
        await _context.SaveChangesAsync();
    }
}