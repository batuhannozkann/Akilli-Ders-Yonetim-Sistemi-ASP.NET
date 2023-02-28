using Adys.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Core.Repositories
{
    public interface ILessonRepository:IGenericRepository<Lesson>
    {
        Task<List<Lesson>> GetLessonsWithAcademician();
        Task<Lesson> GetLesson(int id); 
        Task<List<Lesson>> GetAllLessonWithFiles();
    }
}
