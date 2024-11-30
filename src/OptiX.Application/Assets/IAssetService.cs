namespace OptiX.Application.Assets;

public interface IAssetService
{
    Task CreateAsync(AssetDto asset);
    Task<List<AssetDto>> GetAllAsync();
}