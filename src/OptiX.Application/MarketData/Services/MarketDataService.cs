using Microsoft.EntityFrameworkCore;
using OptiX.Application.MarketData.Requests;
using OptiX.Application.MarketData.Responses;
using OptiX.Domain.Entities.Asset;
using OptiX.Domain.ValueObjects;
using Optix.Infrastructure.Database;

namespace OptiX.Application.MarketData.Services;

public sealed class MarketDataService : IMarketDataService
{
    private readonly AppDbContext _context;

    public MarketDataService(AppDbContext context)
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

    public async Task<List<PriceCandleDto>> GetPriceCandlesAsync(GetMarketDataRequest request)
    {
        var fromUtc = request.From.ToUniversalTime();
        var toUtc = request.To.ToUniversalTime();
        
        var ticks = await _context.Ticks
            .AsNoTracking()
            .Where(tick => tick.AssetId == request.AssetId && tick.Date >= fromUtc && tick.Date <= toUtc)
            .ToListAsync();
        
        return BuildCandles(ticks, request.TimeFrame);
    }

    private static List<PriceCandleDto> BuildCandles(IEnumerable<Tick> ticks, TimeFrame timeFrame)
    {
        return ticks
            .GroupBy(t => new
            {
                t.AssetId,
                IntervalStart = Truncate(t.Date, timeFrame)
            })
            .Select(g => new PriceCandleDto(g.Key.IntervalStart,
                g.First().Price,
                g.Max(t => t.Price),
                g.Min(t => t.Price),
                g.Last().Price,
                g.Sum(t => t.Volume)))
            .OrderBy(c => c.StartTime)
            .ToList();
    }

    private static DateTime Truncate(DateTime dateTime, TimeFrame timeFrame)
    {
        var interval = timeFrame switch
        {
            TimeFrame.Tick => TimeSpan.FromTicks(1),
            TimeFrame.Second => TimeSpan.FromSeconds(1),
            TimeFrame.Minute => TimeSpan.FromMinutes(1),
            TimeFrame.Hour => TimeSpan.FromHours(1),
            _ => throw new ArgumentOutOfRangeException(nameof(timeFrame), $"Unsupported TimeFrame: {timeFrame}")
        };

        var truncatedTicks = dateTime.Ticks / interval.Ticks * interval.Ticks;
        return new DateTime(truncatedTicks, dateTime.Kind);
    }
}