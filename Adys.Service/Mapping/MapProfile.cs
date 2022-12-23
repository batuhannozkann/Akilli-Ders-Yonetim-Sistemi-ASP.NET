using Adys.Core.DTOs;
using Adys.Core.Entities;
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
        }
    }
}
