using Adys.Core.DTOs;
using Adys.Core.Entities;
using Adys.Core.Repositories;
using Adys.Core.Services;
using Adys.Core.UnitOfWork;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Service.Services
{
    public class StudentService : Service<Student>, IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private IMapper _mapper;
        public StudentService(IGenericRepository<Student> genericRepository, IUnitOfWork unitOfWork, IStudentRepository studentRepository, IMapper mapper) : base(genericRepository, unitOfWork)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }
        public async Task<CustomResponseDto<List<LessonWithStudentsDto>>> GetLessonsOfStudents(long studentNumber)
        {
            var lessons = await _studentRepository.GetLessonsOfStudent(studentNumber);
            var lessonsDto = _mapper.Map<List<LessonWithStudentsDto>>(lessons);
            return CustomResponseDto<List<LessonWithStudentsDto>>.Succes(statusCode: 200, data: lessonsDto);
        }
    }
}
