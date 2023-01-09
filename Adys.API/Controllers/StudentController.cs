using Adys.Core.DTOs;
using Adys.Core.Entities;
using Adys.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Adys.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IStudentService _service;
        private readonly ILessonStudentService _lessonStudentService;

        public StudentController(IMapper mapper, IStudentService service, ILessonStudentService lessonStudentService)
        {
            _mapper = mapper;
            _service = service;
            _lessonStudentService = lessonStudentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _service.GetAllAsync();
            var studentsDto = _mapper.Map<List<StudentDto>>(students);
            return CreateActionResult(CustomResponseDto<List<StudentDto>>.Succes(200, studentsDto));
        }
        [HttpGet("[action]/{studentNumber}")]
        public async Task<IActionResult> GetLessonsOfStudent(string studentNumber)
        {
            return CreateActionResult<List<LessonWithStudentsDto>>(await _service.GetLessonsOfStudents(Convert.ToInt64(studentNumber)));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteLessonOfStudent(LessonStudentAboutDto lessonStudentDto)
        {
            return CreateActionResult(await _lessonStudentService.RemoveLessonStudent(lessonStudentDto));
            
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddLessonOfStudent(List<LessonStudentAboutDto> lessonStudentDtos)
        {
            return CreateActionResult(await _lessonStudentService.AddLessonStudent(lessonStudentDtos));
        }
    }
}
