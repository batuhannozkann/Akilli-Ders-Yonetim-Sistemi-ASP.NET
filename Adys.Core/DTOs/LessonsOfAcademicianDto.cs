﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Core.DTOs
{
    public class LessonsOfAcademicianDto:AcademicianDto
    {
        public ICollection<LessonDto> Lessons { get; set; }
    }
}
