using Adys.Core.DTOs;
using Adys.Core.Identity;
using Adys.Core.Identity.DTOs;
using Adys.Core.Identity.Service;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<UserApp> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new UserApp()
            {
                Email = createUserDto.Email,
                UserName = createUserDto.StudentNumber.ToString(),
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                StudentNumber= createUserDto.StudentNumber,
            };
            var result = await _userManager.CreateAsync(user, createUserDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return CustomResponseDto<UserAppDto>.Fail(statusCode: 400, errors);
            }
            var userAppDto = _mapper.Map<UserAppDto>(user);
            return CustomResponseDto<UserAppDto>.Succes(statusCode: 200, data: userAppDto);

        }

        public async Task<CustomResponseDto<UserAppDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return CustomResponseDto<UserAppDto>.Fail(statusCode: 400, errors: new List<string> { "User is not found" }); var userAppDto = _mapper.Map<UserAppDto>(user);
            return CustomResponseDto<UserAppDto>.Succes(statusCode: 200, data: userAppDto);
        }
        public async Task<CustomResponseDto<String>> ResetPassword(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return CustomResponseDto<String>.Fail(statusCode: 201, "Username is wrong");
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            return CustomResponseDto<String>.Succes(statusCode: 200, data: resetToken);
        }
    
           
    }
}
