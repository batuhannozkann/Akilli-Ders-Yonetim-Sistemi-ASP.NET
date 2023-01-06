using Adys.Core.DTOs;
using Adys.Core.Identity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Core.Identity.Service
{
    public interface IAuthenticationService
    {
        Task<CustomResponseDto<TokenDto>>CreateTokenAsync(LoginDto loginDto);
        Task<CustomResponseDto<TokenDto>>CreateTokenByRefreshToken(string refreshToken);
        Task<CustomNoResponseDto> RevokeRefreshToken(string refreshToken);
        Task<CustomResponseDto<ClientTokenDto>> CreateTokenByClient(ClientLoginDto clientLoginDto);
        
    }
}
