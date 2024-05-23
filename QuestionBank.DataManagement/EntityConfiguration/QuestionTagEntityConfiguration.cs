using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestionBank.Persistence.Entity;

namespace QuestionBank.DataManagement.EntityConfiguration
{
    public class QuestionTagEntityConfiguration : IEntityTypeConfiguration<QuestionTag>
    {
        public void Configure(EntityTypeBuilder<QuestionTag> builder)
        {
            builder.ToTable("QuestionTag");
            builder.Property(_ => _.TagId).IsRequired();

            builder.Property(_ => _.QuestionId).IsRequired();

        }
    }
}
