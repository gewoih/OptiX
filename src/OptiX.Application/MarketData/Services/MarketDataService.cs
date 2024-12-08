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
        var marketDataToSave = ticks.Select(tick => new Tick
        {
            Id = tick.Id,
            Symbol = tick.Symbol,
            Price = tick.Price,
            Volume = tick.Volume,
            Date = tick.Date
        });

        await _context.Ticks.AddRangeAsync(marketDataToSave);
        await _context.SaveChangesAsync();
    }

    public async Task<TickDto?> GetLastTickAsync(string symbol)
    {
        var lastTick = await _context.Ticks
            .Where(tick => tick.Symbol == symbol)
            .OrderByDescending(tick => tick.Date)
            .Select(tick => new TickDto
            {
                Symbol = tick.Symbol,
                Price = tick.Price,
                Volume = tick.Volume,
                Date = tick.Date
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
            .Where(tick => tick.Symbol == request.Symbol && tick.Date >= fromUtc && tick.Date <= toUtc)
            .Select(tick => new Tick
            {
                Date = tick.Date,
                Price = tick.Price,
                Volume = tick.Volume
            })
            .ToListAsync();
        
        return BuildCandles(ticks, request.TimeFrame);
    }

    private static List<PriceCandleDto> BuildCandles(IEnumerable<Tick> ticks, TimeFrame timeFrame)
    {
        return ticks
            .GroupBy(t => new
            {
                IntervalStart = Truncate(t.Date, timeFrame)
            })
            .Select(g => new PriceCandleDto(g.Key.IntervalStart,
                g.First().Price,
                g.Max(t => t.Price),
                g.Min(t => t.Price),
                g.Last().Price,
                g.Sum(t => t.Volume)))
            .OrderBy(c => c.Date)
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