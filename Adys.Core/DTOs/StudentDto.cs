using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Core.DTOs
{
    public class StudentDto:BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long StudentNumber { get; set; }
    }
}
