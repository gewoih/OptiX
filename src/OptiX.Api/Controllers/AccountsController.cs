using Microsoft.AspNetCore.Mvc;
using OptiX.Application.Account;

namespace OptiX.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            await _accountService.CreateAsync(request);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllAccountsRequest request)
        {
            var accounts = await _accountService.GetAllAsync(request);
            return Ok(accounts);
        }
    }
}
