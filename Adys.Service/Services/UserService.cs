using Adys.Core.DTOs;
using Adys.Core.Identity;
using Adys.Core.Identity.DTOs;
using Adys.Core.Identity.Service;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
                StudentNumber = createUserDto.StudentNumber,
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
            await _roleManager.CreateAsync(new IdentityRole { Name = roleNames });
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
            var encodedToken = Encoding.UTF8.GetBytes(resetToken);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedToken);
            mailMessage.Body = $"Şifrenizi Sıfırlamak için <a href='http://localhost:3000/SifremiSifirla/{validEmailToken}/{user.UserName}'>Tıklayınız</a>";
            mailMessage.IsBodyHtml = true;
            SmtpClient smp = new SmtpClient();
            smp.Credentials = new NetworkCredential("batu.besiktas@live.com", "");
            smp.Port = 587;
            smp.Host = "smtp.office365.com";
            smp.UseDefaultCredentials = false;
            smp.EnableSsl = true;
            smp.Send(mailMessage);
            return CustomResponseDto<String>.Succes(statusCode: 200, data: resetToken);
        }

        public async Task<CustomNoResponseDto> UpdatePassword(string password, string token, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) throw new Exception("User not found");
            var decodeToken = WebEncoders.Base64UrlDecode(token);
            var decodedToken = Encoding.UTF8.GetString(decodeToken);
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, password);
            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(user);
            }
            return CustomNoResponseDto.Succes(200);
        }
        public async Task<CustomResponseDto<IList<UserApp>>> GetAllUsers()
        {
            var userList = _userManager.Users.ToList();
            return CustomResponseDto<IList<UserApp>>.Succes(200, userList);
        }
        public CustomResponseDto<IList<IdentityRole>> GetAllRoles()
        {
            var roleList = _roleManager.Roles.ToList();
            return CustomResponseDto<IList<IdentityRole>>.Succes(200, roleList);
        }
        public async Task<CustomResponseDto<IList<String>>> GetUserRolesAsync(string id)
        {
            var user = _userManager.Users.Where(x => x.Id == id).FirstOrDefault();
            if (user == null) throw new Exception("User doesn't exist");
            var userRoleList = await _userManager.GetRolesAsync(user);
            return CustomResponseDto<IList<String>>.Succes(200, userRoleList);

        }
        public async Task<CustomNoResponseDto> ClaimRoleToUser(IList<string> roleId,string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new Exception("User doesn't exist");
            List<IdentityRole> userRoleList = new List<IdentityRole>();
            foreach(var i in roleId)
            {
                userRoleList.Add(await _roleManager.FindByIdAsync(i));
            }
            if (userRoleList == null) throw new Exception("Role doesn't exist");
            await _userManager.AddToRolesAsync(user, userRoleList.Select(x => x.Name));
            return CustomNoResponseDto.Succes(201);
        }
        public async Task<CustomNoResponseDto> DeleteRoleFromUser(string role,string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.RemoveFromRoleAsync(user, role);
            if(result.Succeeded)
            {
                return CustomNoResponseDto.Succes(201);
            }
            return CustomNoResponseDto.Fail(400, "Error");
        }
        public async Task<CustomResponseDto<UserAppDto>> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userAppDto = new UserAppDto();
            userAppDto.FirstName = user.FirstName;
            userAppDto.Id = id;
            userAppDto.LastName = user.LastName;

            return CustomResponseDto<UserAppDto>.Succes(200,userAppDto);
        }
        public async Task<CustomResponseDto<UserAppDto>> EditUserInfo(UserAppDto userAppDto)
        {
            var user = await _userManager.FindByIdAsync(userAppDto.Id);
            if (userAppDto.FirstName == null && userAppDto.LastName == null) return CustomResponseDto<UserAppDto>.Fail(400, "Fields are required");
            user.FirstName = userAppDto.FirstName;
            user.LastName = userAppDto.LastName;
            await _userManager.UpdateAsync(user);
            return CustomResponseDto<UserAppDto>.Succes(201,userAppDto);
        }
        public async Task<CustomResponseDto<UserApp>> DeleteUser(UserAppDto userAppDto)
        {
            var user = await _userManager.FindByIdAsync(userAppDto.Id);
            if (user == null) return CustomResponseDto<UserApp>.Fail(400, "User doesn't exist");
            var result = await _userManager.DeleteAsync(user);
            if(result.Succeeded)
            {
                return CustomResponseDto<UserApp>.Succes(201, user);
            }
            return CustomResponseDto<UserApp>.Fail(400, "user can't delete");
             
        }
          
            


    
           
    }
}
