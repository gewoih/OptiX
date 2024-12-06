namespace OptiX.Domain.Entities.Asset;

public sealed class Tick
{
    public long Id { get; set; }
    public string Symbol { get; set; }
    public long TimeStamp { get; set; }
    public decimal Price { get; set; }
    public decimal Volume { get; set; }
}