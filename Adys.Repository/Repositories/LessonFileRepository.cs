using Adys.Core.Entities;
using Adys.Core.Repositories;
using Adys.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Repository.Repositories
{
    public class LessonFileRepository : GenericRepository<LessonFile>, ILessonFileRepository
    {
        public LessonFileRepository(AdysAppContext context) : base(context)
        {

        }
        public async Task AddFileAsync(int lessonId,string fileName,string fileUrl)
        {
            var lesson= await _context.Lessons.FindAsync(lessonId);
            await _context.LessonFile.AddAsync(new LessonFile { FileName = fileName, Lesson = lesson, FileUrl = fileUrl, LessonId = lessonId });

        }
    }
}
