using Adys.Core.Identity;
using Adys.Core.Repositories;
using Adys.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Repository.Repositories
{
    public class UserRefreshTokenRepository : GenericRepository<UserRefreshToken>, IGenericRepository<UserRefreshToken>
    {
        public UserRefreshTokenRepository(AdysAppContext context) : base(context)
        {

        }
    }
}
