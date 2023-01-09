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
    public class LessonStudentRepository : GenericRepository<LessonStudent>, ILessonStudentRepository
    {
        public LessonStudentRepository(AdysAppContext context) : base(context)
        {

        }
    }
}
