using Adys.Core.DTOs;
using Adys.Core.Identity.DTOs;
using Adys.Core.Identity.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Adys.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserDto createUserDto)
        {
            return CreateActionResult(await _userService.CreateUserAsync(createUserDto));
        }
        [HttpGet]
        public async Task<IActionResult> GetUser(string userName)
        {
            return CreateActionResult(await _userService.GetUserByNameAsync(userName));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> ResetPassword(string userName)
        {
            return CreateActionResult(await _userService.ResetPassword(userName));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            return CreateActionResult(await _userService.CreateUserRoles(roleName));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordDto updatePasswordDto)
        {
            return CreateActionResult(await _userService.UpdatePassword(updatePasswordDto.UserName,updatePasswordDto.Token,updatePasswordDto.Password));
        }
    }
}
