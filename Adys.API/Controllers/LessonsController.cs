using Adys.Core.DTOs;
using Adys.Core.Entities;
using Adys.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Adys.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : BaseController
    { 
        private readonly IMapper _mapper;
        private readonly ILessonService _service;
        private readonly ILessonStudentService _lessonStudentService;
        public LessonsController(IMapper mapper, ILessonService lessonService, ILessonStudentService lessonStudentService)
        {
            _mapper = mapper;
            _service = lessonService;
            _lessonStudentService = lessonStudentService;
        }
        [HttpGet]
        [EnableCors]
        public async Task<IActionResult> All()
        {
            var lessons = await _service.GetAllAsync();
            var lessonDtos = _mapper.Map<List<LessonDto>>(lessons);
            return CreateActionResult<List<LessonDto>>(CustomResponseDto<List<LessonDto>>.Succes(statusCode: 200, data: lessonDtos));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> LessonsWithAcademician()
        {
            return CreateActionResult<List<LessonWithAcademicianDto>>(await _service.GetLessonsWithAcademician());
        }
        [HttpPost]
        public async Task<IActionResult> Add(LessonDto lessonDto)
        {
            var lesson = await _service.AddAsync(_mapper.Map<Lesson>(lessonDto));
            lessonDto = _mapper.Map<LessonDto>(lesson);
            return CreateActionResult<LessonDto>(CustomResponseDto<LessonDto>.Succes(statusCode:201,data:lessonDto));
            
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddLessonsToStudent(List<LessonStudentDto> lessonStudents)
        {
            var lessonStudentList = _mapper.Map<List<LessonStudent>>(lessonStudents);
            var LessonStudents = await _lessonStudentService.AddRangeAsync(lessonStudentList);
            var lessonStudentDto = _mapper.Map<List<LessonStudentDto>>(LessonStudents);
            return CreateActionResult<List<LessonStudentDto>>(CustomResponseDto<List<LessonStudentDto>>.Succes(statusCode: 200, data: lessonStudentDto));

        }
    }
}
