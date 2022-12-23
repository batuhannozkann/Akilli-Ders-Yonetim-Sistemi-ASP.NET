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
    public class AcademicianService : Service<Academician>, IAcademicianService
    {
        private readonly IAcademicianRepository _academicianRepository;
        IMapper _mapper;

        public AcademicianService(IUnitOfWork unitOfWork, IAcademicianRepository academicianRepository, IMapper mapper, IGenericRepository<Academician> repository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _academicianRepository = academicianRepository;
        }

        public async Task<CustomResponseDto<List<LessonsOfAcademicianDto>>> GetLessonsOfAcademician()
        {
            var LessonsOfAcademician = await _academicianRepository.GetLessonsWithAcademician();
            var LessonsOfAcademicianDto = _mapper.Map<List<LessonsOfAcademicianDto>>(LessonsOfAcademician);
            return CustomResponseDto<List<LessonsOfAcademicianDto>>.Succes(statusCode: 200, data: LessonsOfAcademicianDto);

        }
    }
}
