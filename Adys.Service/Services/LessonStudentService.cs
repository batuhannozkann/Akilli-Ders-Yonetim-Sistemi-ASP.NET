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
    public class LessonStudentService : Service<LessonStudent>,ILessonStudentService
    {
        private ILessonStudentRepository _lessonStudentRepository;
        private IStudentRepository _studentRepository;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        public LessonStudentService(IGenericRepository<LessonStudent> genericRepository, IUnitOfWork unitOfWork, ILessonRepository lessonRepository, IMapper mapper, ILessonStudentRepository lessonStudentRepository, IStudentRepository studentRepository) : base(genericRepository, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _lessonStudentRepository = lessonStudentRepository;
            _studentRepository = studentRepository;
        }
        public async Task<CustomNoResponseDto> RemoveLessonStudent(LessonStudentAboutDto lessonStudentAboutDto)
        {
            var student = _studentRepository.Where(x => x.StudentNumber == Convert.ToInt64(lessonStudentAboutDto.StudentNumber)).FirstOrDefault();
            var lessonStudent = _lessonStudentRepository.Where(c => c.StudentId == student.Id && c.LessonId == int.Parse(lessonStudentAboutDto.LessonId)).FirstOrDefault();
            if (lessonStudent == null) throw new Exception("Wrong request");
            _lessonStudentRepository.Remove(lessonStudent);
            _unitOfWork.Commit();
            return CustomNoResponseDto.Succes(200);
        }
        public async Task<CustomNoResponseDto> AddLessonStudent(List<LessonStudentAboutDto> LessonStudentAboutDtos)
        {
            if (LessonStudentAboutDtos == null) throw new ArgumentNullException("lessonStudent is null");
            foreach(var i in LessonStudentAboutDtos)
            {
                var student = _studentRepository.Where(x => x.StudentNumber == Convert.ToInt64(i.StudentNumber)).FirstOrDefault();
                if (student == null) throw new Exception("Student doesn't exist");
                await _lessonStudentRepository.AddAsync(new LessonStudent { LessonId = int.Parse(i.LessonId), StudentId = student.Id });
            }
            _unitOfWork.CommitAsync();
            return CustomNoResponseDto.Succes(200);
        }
    }
}
