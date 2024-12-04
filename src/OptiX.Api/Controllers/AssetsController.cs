using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OptiX.Application.Assets.Responses;
using OptiX.Application.Assets.Services;

namespace OptiX.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetService _assetService;

        public AssetsController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AssetDto assetDto)
        {
            await _assetService.CreateAsync(assetDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var assets = await _assetService.GetAllAsync();
            return Ok(assets);
        }
    }
}
