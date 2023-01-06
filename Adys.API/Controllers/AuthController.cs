using Adys.Core.DTOs;
using Adys.Core.Identity.DTOs;
using Adys.Core.Identity.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Adys.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            return CreateActionResult(await _authenticationService.CreateTokenAsync(loginDto));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateTokenByRefreshToken (string refreshToken)
        {
            return CreateActionResult(await _authenticationService.CreateTokenByRefreshToken(refreshToken));
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("[action]")]
        public async Task<IActionResult> AuthValid ()
        {
            return Ok("Authenticated");
        }
    }
}
