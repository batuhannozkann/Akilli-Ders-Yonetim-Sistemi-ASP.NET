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
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<UserApp> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
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

        public async Task<CustomNoResponseDto> CreateUserRoles(string roleNames)
        {
            if (await _roleManager.RoleExistsAsync(roleNames)) return CustomNoResponseDto.Fail(412, "Your added role is exist");
            await _roleManager.CreateAsync(new IdentityRole { Name=roleNames});
            return CustomNoResponseDto.Succes(statusCode: 200);
        }

        public async Task<CustomResponseDto<UserAppDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return CustomResponseDto<UserAppDto>.Fail(statusCode: 400, errors: new List<string> { "User is not found" }); var userAppDto = _mapper.Map<UserAppDto>(user);
            return CustomResponseDto<UserAppDto>.Succes(statusCode: 200, data: userAppDto);
        }
        public async Task<CustomResponseDto<String>> ResetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return CustomResponseDto<String>.Fail(statusCode: 201, "Username is wrong");
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            MailMessage mailMessage = new MailMessage();
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(user.Email);
            mailMessage.From = new MailAddress("batu.besiktas@live.com", "Şifre Güncelleme", System.Text.Encoding.UTF8);
            mailMessage.Subject = "Şifre Güncelleme";
            mailMessage.Body = $"Şifrenizi Sıfırlamak için <a href='http://localhost:3000/SifremiSifirla/{resetToken}/{user.UserName}'>Tıklayınız</a>";
            mailMessage.IsBodyHtml = true;
            SmtpClient smp = new SmtpClient();
            smp.Credentials = new NetworkCredential("batu.besiktas@live.com", "02042503900");
            smp.Port = 587;
            smp.Host = "smtp.office365.com";
            smp.UseDefaultCredentials = false;
            smp.EnableSsl = true;
            smp.Send(mailMessage);
            return CustomResponseDto<String>.Succes(statusCode: 200, data: resetToken);
        }
        
        public async Task<CustomNoResponseDto> UpdatePassword(string password,string token,string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var result = await _userManager.ResetPasswordAsync(user, token, password);
            if(result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(user);
            }
            return CustomNoResponseDto.Succes(200);
        }
    
           
    }
}
