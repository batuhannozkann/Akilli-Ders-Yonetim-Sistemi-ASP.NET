using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Core.Entities
{
    public class LessonFile
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public Lesson? Lesson { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
    }
}
