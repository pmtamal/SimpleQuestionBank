using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestionBank.Persistence.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.DataManagement.EntityConfiguration
{
    public class UserRefreshEntityConfiguration : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.ToTable("UserRefreshToken");

            builder.Property(_ => _.UserName).IsRequired().HasMaxLength(200);
            
            builder.Property(_=>_.RefreshToken).IsRequired().HasMaxLength(500);           
            


        }
    }
}
