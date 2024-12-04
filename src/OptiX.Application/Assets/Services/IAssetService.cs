using OptiX.Application.Assets.Requests;
using OptiX.Application.Assets.Responses;

namespace OptiX.Application.Assets.Services;

public interface IAssetService
{
    Task<AssetDto> CreateAsync(CreateAssetRequest request);
    Task<List<AssetDto>> GetAllAsync();
    Task<AssetDto?> GetAsync(Guid id);
}