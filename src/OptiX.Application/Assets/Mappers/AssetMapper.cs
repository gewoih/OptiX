using OptiX.Application.Assets.Responses;
using OptiX.Domain.Entities.Asset;

namespace OptiX.Application.Assets.Mappers;

public static class AssetMapper
{
    public static AssetDto ToDto(this Asset asset)
    {
        return new AssetDto(asset.Id, asset.Symbol);
    }
}