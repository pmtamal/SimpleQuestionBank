using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestionBank.Persistence.Entity;

namespace QuestionBank.DataManagement.EntityConfiguration
{
    public class UserTagEntityConfiguration : IEntityTypeConfiguration<UserTag>
    {
        public void Configure(EntityTypeBuilder<UserTag> builder)
        {
            builder.ToTable("UserTag");
            

        }
    }
}
