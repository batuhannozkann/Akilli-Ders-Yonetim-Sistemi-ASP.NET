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
    public class LessonRepository : GenericRepository<Lesson>,ILessonRepository
    {
        public LessonRepository(AdysAppContext context) : base(context)
        {
        }

        public async Task<List<Lesson>> GetLessonsWithAcademician()
        {
            return await _context.Lessons.Include(c => c.Academician).ToListAsync();
        }
    }
}
