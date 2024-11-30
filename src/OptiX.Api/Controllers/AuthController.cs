using Microsoft.AspNetCore.Mvc;
using OptiX.Application.Users;

namespace OptiX.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("google")]
        public IActionResult GoogleLogin(string returnUrl)
        {
            var redirectUrl = Url.Action(nameof(GoogleCallback), "Auth", new { ReturnUrl = returnUrl });
            var properties = _userService.GetGoogleAuthenticationProperties(redirectUrl);
            return Challenge(properties, "Google");
        }

        [HttpGet("signin-google")]
        public async Task<IActionResult> GoogleCallback(string returnUrl)
        {
            var token = await _userService.LoginGoogleAsync();
            if (string.IsNullOrEmpty(token))
                return Unauthorized();
        
            return Redirect($"{returnUrl}?token={token}");
        }
    }
}
