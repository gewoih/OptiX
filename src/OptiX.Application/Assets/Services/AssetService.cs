using Microsoft.EntityFrameworkCore;
using OptiX.Application.Assets.Mappers;
using OptiX.Application.Assets.Requests;
using OptiX.Application.Assets.Responses;
using OptiX.Domain.Entities.Asset;
using Optix.Infrastructure.Database;

namespace OptiX.Application.Assets.Services;

public sealed class AssetService : IAssetService
{
    private readonly AppDbContext _context;

    public AssetService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AssetDto> CreateAsync(CreateAssetRequest request)
    {
        var asset = new Asset { Symbol = request.Symbol };

        await _context.Assets.AddAsync(asset);
        await _context.SaveChangesAsync();

        return asset.ToDto();
    }

    public async Task<List<AssetDto>> GetAllAsync()
    {
        var assets = await _context.Assets
            .Select(asset => asset.ToDto())
            .ToListAsync();

        return assets;
    }

    public async Task<AssetDto?> GetAsync(Guid id)
    {
        var asset = await _context.Assets.FindAsync(id);
        return asset?.ToDto();
    }
}