using Adys.Core.Configuration;
using Adys.Core.DTOs;
using Adys.Core.Identity;
using Adys.Core.Identity.DTOs;
using Adys.Core.Identity.Service;
using Adys.Core.Repositories;
using Adys.Core.Services;
using Adys.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenService;

        public AuthenticationService(IOptions<List<Client>> clients, ITokenService tokenService, UserManager<UserApp> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRefreshTokenService)
        {
            _clients = clients.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshTokenService = userRefreshTokenService;
        }

        public async Task<CustomResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return CustomResponseDto<TokenDto>.Fail(statusCode: 400, errors: new List<string>() { "Email or Password is wrong" });
            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password)) return CustomResponseDto<TokenDto>.Fail(statusCode: 400, errors: new List<string>() { "Email or Password is wrong" });
            var token = _tokenService.CreateToken(user);
            var userRefreshToken = await _userRefreshTokenService.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();
            if (userRefreshToken == null) await _userRefreshTokenService.AddAsync(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<TokenDto>.Succes(statusCode: 200, data: token);
        }

        public Task<CustomResponseDto<ClientTokenDto>> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomResponseDto<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();
            if (existRefreshToken == null) return CustomResponseDto<TokenDto>.Fail(statusCode: 404, errors: new List<string> { "Refresh token not found" });
            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);
            if (user == null) return CustomResponseDto<TokenDto>.Fail(statusCode: 404, errors: new List<string> { "UserId not found" });
            var tokenDto = _tokenService.CreateToken(user);
            existRefreshToken.Code = tokenDto.RefreshToken;
            existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<TokenDto>.Succes(statusCode: 200, data: tokenDto);
        }

        public async Task<CustomNoResponseDto> RevokeRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();
            if (existRefreshToken == null) return CustomNoResponseDto.Fail(statusCode: 404, errors: new List<string> { "Refresh token not found" });
            _userRefreshTokenService.Remove(existRefreshToken);
            return CustomNoResponseDto.Succes(statusCode: 200);
        }
    }
}
