using Adys.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Core.Repositories
{
    public interface ILessonFileRepository:IGenericRepository<LessonFile>
    {
        Task AddFileAsync(int lessonId, string fileName, string fileUrl);
    }
}
