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
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(AdysAppContext context) : base(context)
        {
        }
        public async Task<List<Student>> GetLessonsOfStudent(long studentNumber)
        {
            var students = await _context.Students.Where(c => c.StudentNumber == studentNumber).Include(c => c.Lessons).ThenInclude(c => c.Lesson).ToListAsync();
            return students;
        }
    }
}
