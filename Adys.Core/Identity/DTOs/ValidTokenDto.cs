using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Core.Identity.DTOs
{
    public class ValidTokenDto
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }
}
