using Microsoft.EntityFrameworkCore;
using Optix.Infrastructure.Database;

namespace OptiX.Application.Ticks.Services;

public sealed class TickService : ITickService
{
    private readonly AppDbContext _context;

    public TickService(AppDbContext context)
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

    public async Task<TickDto?> GetLastTickAsync(Guid assetId)
    {
        var lastTick = await _context.Ticks
            .Where(tick => tick.AssetId == assetId)
            .OrderByDescending(tick => tick.Date)
            .Select(tick => new TickDto
            {
                AssetId = tick.AssetId,
                Price = tick.Price,
                Volume = tick.Volume,
                DateTime = tick.Date
            })
            .FirstOrDefaultAsync();
        
        return lastTick;
    }
}