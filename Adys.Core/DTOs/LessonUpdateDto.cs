using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Core.DTOs
{
    public class LessonUpdateDto:BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int AcademicianId { get; set; }
        public string LessonCode { get; set; }
    }
}
