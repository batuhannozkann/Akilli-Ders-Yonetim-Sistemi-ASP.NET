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
    public class AcademiciansController : BaseController
    {
        private readonly IService<Academician> _service;
        private readonly IMapper _mapper;

        public AcademiciansController(IService<Academician> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var academicians = await _service.GetAllAsync();
            var academiciansDto = _mapper.Map<List<AcademicianDto>>(academicians);
            return CreateActionResult<List<AcademicianDto>>(CustomResponseDto<List<AcademicianDto>>.Succes(200, academiciansDto));

        }
        [HttpPost]
        public async Task<IActionResult> Add(AcademicianDto academicianDto)
        {
            var Academician = _mapper.Map<Academician>(academicianDto);
            var addedAcademician = await _service.AddAsync(Academician);
            var addedAcademicianDto = _mapper.Map<AcademicianDto>(addedAcademician);
            return CreateActionResult<AcademicianDto>(CustomResponseDto<AcademicianDto>.Succes(201, data: addedAcademicianDto));
        }
    }
}
