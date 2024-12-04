using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OptiX.Application.Accounts.Requests;
using OptiX.Application.Accounts.Services;

namespace OptiX.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAccountRequest request)
        {
            var createdAccount = await _accountService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = createdAccount.Id }, createdAccount);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllAccountsRequest request)
        {
            var accounts = await _accountService.GetAllAsync(request);
            return Ok(accounts);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var accounts = await _accountService.GetAsync(id);
            return Ok(accounts);
        }
    }
}
