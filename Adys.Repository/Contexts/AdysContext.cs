using Adys.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Repository.Contexts
{
    public class AdysAppContext : DbContext
    {
        public AdysAppContext(DbContextOptions<AdysAppContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());





            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Academician> Academicians { get; set; }
        public DbSet<LessonStudent> LessonStudent { get; set; }
    }
}
