using Adys.Core.Identity;
using Adys.Core.Repositories;
using Adys.Core.Services;
using Adys.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Service.Services
{
    public class UserRefreshTokenService : IdentityGenericService<UserRefreshToken>, IUserRefreshTokenService
    {
        public UserRefreshTokenService(IIdentityGenericRepository<UserRefreshToken> genericRepository, IUnitOfWork unitOfWork) : base(genericRepository, unitOfWork)
        {
        }
    }
}
