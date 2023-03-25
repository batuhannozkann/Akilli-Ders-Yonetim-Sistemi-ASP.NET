using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Core.DTOs
{
    public class DeleteRoleFromUserDto
    {
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}
