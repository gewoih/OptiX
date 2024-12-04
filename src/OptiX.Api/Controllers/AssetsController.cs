using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OptiX.Application.Assets.Requests;
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
        public async Task<IActionResult> Post([FromBody] CreateAssetRequest request)
        {
            var asset = await _assetService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = asset.Id }, asset);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var assets = await _assetService.GetAllAsync();
            return Ok(assets);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var asset = await _assetService.GetAsync(id);
            if (asset is null)
                return NotFound();
            
            return Ok(asset);
        }
    }
}
