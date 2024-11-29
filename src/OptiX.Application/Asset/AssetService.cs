using Microsoft.EntityFrameworkCore;
using Optix.Infrastructure.Database;

namespace OptiX.Application.Asset;

public sealed class AssetService : IAssetService
{
    private readonly AppDbContext _context;

    public AssetService(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(AssetDto asset)
    {
        var newAsset = new Domain.Entities.Asset.Asset
        {
            Symbol = asset.Symbol
        };
        
        await _context.Assets.AddAsync(newAsset);
        await _context.SaveChangesAsync();
    }

    public async Task<List<AssetDto>> GetAllAsync()
    {
        var assets = await _context.Assets.Select(asset => new AssetDto
        {
            Id = asset.Id,
            Symbol = asset.Symbol
        }).ToListAsync();

        return assets;
    }
}