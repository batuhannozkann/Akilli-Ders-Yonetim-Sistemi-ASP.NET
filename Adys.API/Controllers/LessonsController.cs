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
    public class LessonsController : BaseController
    { 
        private readonly IMapper _mapper;
        private readonly ILessonService _service;
        public LessonsController(IMapper mapper, ILessonService lessonService)
        {
            _mapper = mapper;
            _service = lessonService;
        }
        [HttpGet]
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
    }
}
