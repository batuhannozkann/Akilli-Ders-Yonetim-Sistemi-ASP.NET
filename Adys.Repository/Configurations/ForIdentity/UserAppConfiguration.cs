using Adys.Core.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Repository.Configurations.ForIdentity
{
    public class UserAppConfiguration : IEntityTypeConfiguration<UserApp>
    {
        public void Configure(EntityTypeBuilder<UserApp> builder)
        {
            builder.HasIndex(p => p.StudentNumber).IsUnique();
            builder.Property(p => p.StudentNumber).IsRequired(false);
            builder.Property(p => p.StudentNumber).HasMaxLength(12);
            builder.Property(p => p.FirstName).HasMaxLength(32);
            
        }
    }
}
