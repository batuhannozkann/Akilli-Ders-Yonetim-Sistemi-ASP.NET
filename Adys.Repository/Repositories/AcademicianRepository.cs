using Adys.Core.Entities;
using Adys.Core.Repositories;
using Adys.Repository.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Repository.Repositories
{
    public class AcademicianRepository : GenericRepository<Academician>,IAcademicianRepository
    {
        public AcademicianRepository(AdysAppContext context) : base(context)
        {

        }

        public async Task<List<Academician>> GetLessonsWithAcademician()
        {
            return await _context.Academicians.Include(x => x.Lessons).ToListAsync();
        }
    }
}
