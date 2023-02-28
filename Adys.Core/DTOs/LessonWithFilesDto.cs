using Adys.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Core.DTOs
{
    public class LessonWithFilesDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int AcademicianId { get; set; }
        public string LessonCode { get; set; }
        public AcademicianDto Academician { get; set; }
        public List<LessonFile> LessonFiles { get; set; }
    }
}
