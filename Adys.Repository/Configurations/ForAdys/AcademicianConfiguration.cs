using Adys.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Repository.Configurations.ForAdys
{
    public class AcademicianConfiguration : IEntityTypeConfiguration<Academician>
    {
        public void Configure(EntityTypeBuilder<Academician> builder)
        {
            builder
                .ToTable("Academicians")
                .HasKey(k => k.Id);
            builder
                .HasMany(k => k.Lessons);

        }
    }
}
