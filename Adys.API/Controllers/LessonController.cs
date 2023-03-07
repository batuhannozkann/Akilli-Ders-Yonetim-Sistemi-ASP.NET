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
    public class LessonController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILessonService _service;
        private readonly ILessonStudentService _lessonStudentService;
        private readonly ILessonFileService _lessonFileService;
        public LessonController(IMapper mapper, ILessonService lessonService, ILessonStudentService lessonStudentService, ILessonFileService lessonFileService)
        {
            _mapper = mapper;
            _service = lessonService;
            _lessonStudentService = lessonStudentService;
            _lessonFileService = lessonFileService;
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
            return CreateActionResult<LessonDto>(CustomResponseDto<LessonDto>.Succes(statusCode: 201, data: lessonDto));

        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddLessonsToStudent(List<LessonStudentDto> lessonStudents)
        {
            var lessonStudentList = _mapper.Map<List<LessonStudent>>(lessonStudents);
            var LessonStudents = await _lessonStudentService.AddRangeAsync(lessonStudentList);
            var lessonStudentDto = _mapper.Map<List<LessonStudentDto>>(LessonStudents);
            return CreateActionResult<List<LessonStudentDto>>(CustomResponseDto<List<LessonStudentDto>>.Succes(statusCode: 200, data: lessonStudentDto));

        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLesson(int id)
        {
            return CreateActionResult(await _service.GetLesson(id));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddFileToLesson([FromBody] AddLessonFileDto? lessonFileDto)
        {
            if (lessonFileDto.FileName == "") return CreateActionResult(CustomNoResponseDto.Fail(400, "File is required"));
            return CreateActionResult(await _lessonFileService.AddFileAsync(lessonFileDto));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllLessonWithFiles()
        {
            return CreateActionResult(await _service.GetAllLessonWithFiles());
        }
        [HttpPost("[action]")]
        public  IActionResult DeleteFile(DeleteFileDto deleteFileDto)
        {
            return CreateActionResult(_lessonFileService.DeleteFile(deleteFileDto));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateLesson (LessonUpdateDto lessonUpdateDto)
        {
            if (lessonUpdateDto == null) CreateActionResult(CustomNoResponseDto.Fail(401, "Hatalı Gönderim"));
            return CreateActionResult(_service.EditLesson(lessonUpdateDto));
        }
        
    }
}
