using OptiX.Application.Assets.Responses;

namespace OptiX.Application.Assets.Services;

public interface IAssetService
{
    Task CreateAsync(AssetDto asset);
    Task<List<AssetDto>> GetAllAsync();
}