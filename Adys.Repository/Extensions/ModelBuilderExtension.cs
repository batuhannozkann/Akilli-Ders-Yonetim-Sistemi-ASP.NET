using Adys.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Repository.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lesson>().HasData(
                new Lesson() { Id = 1, Name = "Veri Madenciliği", LessonCode = "BMSB403", AcademicianId=1 ,Description="Veri Madenciligi "},
                    new Lesson() { Id = 2, Name = "Makine Öğrenmesine Giriş", LessonCode = "BMSB406",AcademicianId=1,Description="Makine ögrenmesine giriş" }
            );
            modelBuilder.Entity<Student>().HasData(
                new Student() {Id=1, FirstName = "Batuhan", LastName = "Özkan", CreatedTime = DateTime.Now.Date, StudentNumber = 2180656011 }
                );
            modelBuilder.Entity<Academician>().HasData(
                new Academician { Id = 1, FirstName = "Erkan", LastName = "Özhan", CreatedTime = DateTime.Now.Date, Title = "Dr.Ogr.Uyesi" });
            modelBuilder.Entity<LessonStudent>().HasData(
                new LessonStudent() { LessonId = 1, StudentId = 1 },
                new LessonStudent() { LessonId = 2, StudentId = 1 }
                );
        }
    }
}
