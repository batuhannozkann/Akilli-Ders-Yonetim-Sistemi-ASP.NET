using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Core.DTOs
{
    public class ClaimRoleDto
    {
        public IList<String> RoleIds { get; set; }
        public string UserId { get; set; }
    }
}
