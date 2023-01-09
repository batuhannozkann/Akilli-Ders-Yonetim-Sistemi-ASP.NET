using Adys.Core.DTOs;
using Adys.Core.Entities;
using Adys.Core.Identity;
using Adys.Core.Identity.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Lesson, LessonDto>().ReverseMap();
            CreateMap<Student,StudentDto>().ReverseMap();
            CreateMap<Academician,AcademicianDto>().ReverseMap();
            CreateMap<Lesson, LessonWithAcademicianDto>().ReverseMap();
            CreateMap<Academician, LessonsOfAcademicianDto>().ReverseMap();
            CreateMap<UserApp, UserAppDto>().ReverseMap();
            CreateMap<LessonStudentDto, LessonStudent>().ReverseMap();
            CreateMap<Lesson, LessonWithStudentsDto>().ReverseMap();
            CreateMap<LessonStudent, LessonWithStudentsDto>().ReverseMap();
            CreateMap<Student, LessonWithStudentsDto>().ReverseMap();
            
        }
    }
}
