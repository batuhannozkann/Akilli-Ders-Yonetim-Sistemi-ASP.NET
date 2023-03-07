using Adys.Core.DTOs;
using Adys.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Core.Services
{
    public interface ILessonService:IService<Lesson>
    {
        Task<CustomResponseDto<List<LessonWithAcademicianDto>>> GetLessonsWithAcademician();
        Task<CustomResponseDto<LessonDto>> GetLesson(int id);
        Task<CustomResponseDto<List<LessonDto>>> GetAllLessonWithFiles();
        CustomResponseDto<LessonDto> EditLesson(LessonUpdateDto lessonUpdateDto);
    }
}
