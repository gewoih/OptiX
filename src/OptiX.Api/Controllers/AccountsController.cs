using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OptiX.Application.Accounts.Requests;
using OptiX.Application.Accounts.Services;
using OptiX.Application.Transactions.Services;

namespace OptiX.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        public AccountsController(IAccountService accountService, ITransactionService transactionService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
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

        [HttpGet("{id:guid}/balance")]
        public async Task<IActionResult> GetBalance([FromRoute] Guid id)
        {
            var balance = await _transactionService.GetBalanceAsync(id);
            return Ok(balance);
        }
    }
}
