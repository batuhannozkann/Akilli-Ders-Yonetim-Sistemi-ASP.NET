using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Core.Entities
{
    public class LessonStudent
    {
        public int LessonId { get; set; }
        public int StudentId { get; set; }
        public Lesson Lesson { get; set; }
        public Student Student { get; set; }
    }
}
