using Adys.Core.DTOs;
using Adys.Core.Identity.DTOs;
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


    }
}
