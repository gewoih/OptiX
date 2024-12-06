using Bogus;
using OptiX.Domain.Entities.Asset;
using OptiX.Domain.Entities.Identity;
namespace Optix.Infrastructure.Database;

public class DatabaseSeeder
{
    public static List<Tick> GenerateTicks(int count)
    {
        var faker = new Faker<Tick>()
            .RuleFor(t => t.Id, f => 0)
            .RuleFor(t => t.Symbol, f => "BTCUSDT")
            .RuleFor(t => t.TimeStamp, f =>
            {
                var date = f.Date.Recent();
                return date.Ticks;
            })
            .RuleFor(t => t.Price, f => f.Random.Decimal(90_000m, 100_000m))
            .RuleFor(t => t.Volume, f => f.Random.Decimal());

        return faker.Generate(count);
    }
}