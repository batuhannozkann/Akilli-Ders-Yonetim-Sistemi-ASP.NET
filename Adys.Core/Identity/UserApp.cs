
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Core.Identity
{
    public class UserApp:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Title { get; set; }

    }
}
