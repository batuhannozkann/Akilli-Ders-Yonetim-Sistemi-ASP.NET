using Adys.Core.DTOs;
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
        public async Task<Lesson> GetLesson(int id)
        {
           return await _context.Lessons.Include(c => c.Academician).Include(c => c.LessonFiles).Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<List<Lesson>> GetAllLessonWithFiles()
        {
            return await _context.Lessons.Include(c => c.LessonFiles).Include(c=>c.Academician).ToListAsync();
        }
    }
}
