using Adys.Core.Entities;
using Adys.Repository.Configurations.ForAdys;
using Adys.Repository.Extensions;
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
            modelBuilder.ApplyConfiguration(new AcademicianConfiguration());
            modelBuilder.ApplyConfiguration(new LessonConfiguration());
            modelBuilder.ApplyConfiguration(new LessonStudentConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.SeedData();





            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Academician> Academicians { get; set; }
        public DbSet<LessonStudent> LessonStudent { get; set; }
    }
}
