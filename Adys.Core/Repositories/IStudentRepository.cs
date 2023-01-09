using Adys.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Core.Repositories
{
    public interface IStudentRepository:IGenericRepository<Student>
    {
        Task<List<Student>> GetLessonsOfStudent(long studentNumber);
    }
}
