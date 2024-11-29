namespace OptiX.Application.Asset;

public interface IAssetService
{
    Task CreateAsync(AssetDto asset);
    Task<List<AssetDto>> GetAllAsync();
}