using Adys.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Repository.Configurations
{
    public class LessonStudentConfiguration : IEntityTypeConfiguration<LessonStudent>
    {
        public void Configure(EntityTypeBuilder<LessonStudent> builder)
        {
            builder
                .HasKey(k => new { k.StudentId, k.LessonId });
            builder
                .HasOne(k => k.Lesson)
                .WithMany(c => c.Students)
                .HasForeignKey(fk => fk.LessonId);
            builder
                .HasOne(k => k.Student)
                .WithMany(c => c.Lessons)
                .HasForeignKey(fk => fk.StudentId);
        }
    }
}
