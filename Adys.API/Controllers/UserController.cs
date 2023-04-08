using Adys.Core.DTOs;
using Adys.Core.Identity.DTOs;
using Adys.Core.Identity.Service;
using Adys.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Adys.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IStudentService _studentService;

        public UserController(IUserService userService, IStudentService studentService)
        {
            _userService = userService;
            _studentService = studentService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserDto createUserDto)
        {
           await _studentService.AddAsync(new Core.Entities.Student
            {
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                StudentNumber = createUserDto.StudentNumber
            });
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
            return CreateActionResult(await _userService.UpdatePassword(updatePasswordDto.Password,updatePasswordDto.Token,updatePasswordDto.UserName));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllUsers()
        {
            return CreateActionResult(await _userService.GetAllUsers());
        }
        [HttpGet("[action]")]
        public IActionResult GetAllRoles()
        {
            return CreateActionResult(_userService.GetAllRoles());
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetUserRolesAsync(UserIdDto userId)
        {
            return CreateActionResult(await _userService.GetUserRolesAsync(userId.UserId));
        }
        [HttpPost("[action]")]
        
        public async Task<IActionResult> ClaimRoleToUser(ClaimRoleDto claimRoleDto)
        {
            return CreateActionResult(await _userService.ClaimRoleToUser(claimRoleDto.RoleIds,claimRoleDto.UserId));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteRoleFromUser(DeleteRoleFromUserDto deleteRoleFromUserDto)
        {
            return CreateActionResult(await _userService.DeleteRoleFromUser(deleteRoleFromUserDto.Role, deleteRoleFromUserDto.UserId));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserById (string id)
        {
            return CreateActionResult(await _userService.GetUserById(id));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateUserInfos(UserAppDto userAppDto)
        {
            return CreateActionResult(await _userService.EditUserInfo(userAppDto));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteUser (UserAppDto userAppDto)
        {
            return CreateActionResult(await _userService.DeleteUser(userAppDto));
        }
        
    }
}
