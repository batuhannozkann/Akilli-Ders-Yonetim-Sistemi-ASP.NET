using Adys.Core.DTOs;
using Adys.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Core.Services
{
    public interface IAcademicianService:IGenericRepository<Academician>
    {
        Task<CustomResponseDto<List<LessonsOfAcademicianDto>>> GetLessonsOfAcademician();

    }
}
