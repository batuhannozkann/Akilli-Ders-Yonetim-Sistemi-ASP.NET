using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Core.Entities
{
    public class Lesson : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LessonCode { get; set; }
        public Academician Academician { get; set; }
        public ICollection<LessonStudent> Students { get; set; }
    }
}
