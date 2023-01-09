using Adys.Core.DTOs;
using Adys.Core.Entities;
using Adys.Core.Repositories;
using Adys.Core.Services;
using Adys.Core.UnitOfWork;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Service.Services
{
    public class LessonService : Service<Lesson>, ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IMapper _mapper;
        public LessonService(IUnitOfWork unitOfWork, ILessonRepository lessonRepository, IMapper mapper, IGenericRepository<Lesson> repository) : base(repository, unitOfWork)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<LessonWithAcademicianDto>>> GetLessonsWithAcademician()
        {
            var lessons = await _lessonRepository.GetLessonsWithAcademician();
            var lessonsDto = _mapper.Map<List<LessonWithAcademicianDto>>(lessons);
            return CustomResponseDto<List<LessonWithAcademicianDto>>.Succes(statusCode: 200, data: lessonsDto);
        }
      
    }
}
