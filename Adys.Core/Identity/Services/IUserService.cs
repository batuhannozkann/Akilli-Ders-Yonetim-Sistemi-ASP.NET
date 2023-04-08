using Adys.Core.DTOs;
using Adys.Core.Identity.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Core.Identity.Service
{
    public interface IUserService
    {
        Task<CustomResponseDto<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<CustomResponseDto<UserAppDto>> GetUserByNameAsync(string userName);
        Task<CustomResponseDto<String>> ResetPassword(string Email);
        Task<CustomNoResponseDto> CreateUserRoles(string roleNames);
        Task<CustomNoResponseDto> UpdatePassword(string password, string token, string userName);
        Task<CustomResponseDto<IList<UserApp>>> GetAllUsers();
        CustomResponseDto<IList<IdentityRole>> GetAllRoles();
        Task<CustomResponseDto<IList<String>>> GetUserRolesAsync(string id);
        Task<CustomNoResponseDto> ClaimRoleToUser(IList<string> roleId, string userId);
        Task<CustomNoResponseDto> DeleteRoleFromUser(string role, string userId);
        Task<CustomResponseDto<UserAppDto>> GetUserById(string id);
        Task<CustomResponseDto<UserAppDto>> EditUserInfo(UserAppDto userAppDto);
        Task<CustomResponseDto<UserApp>> DeleteUser(UserAppDto userAppDto);

    }
}
