using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestionBank.Persistence.Entity;

namespace QuestionBank.DataManagement.EntityConfiguration
{
    public class QuestionEntityConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Question");

            builder.Property(_ => _.QuestionCategoryId).IsRequired();

            builder.Property(_ => _.Title).HasMaxLength(200).IsRequired();
            
            builder.Property(_ => _.Description).IsRequired();
            
            builder.Property(_ => _.status).IsRequired();
            
            builder.Property(_ => _.SampleAnswer).IsRequired(false);
            
            
            
            


            builder.HasMany(_ => _.SkillsTags).WithMany(_ => _.Questions).UsingEntity<QuestionTag>
                (l => l.HasOne(_ => _.Tag).WithMany().HasForeignKey(_ => _.TagId),
                r => r.HasOne(_ => _.Question).WithMany(_=>_.QuestionTags).HasForeignKey(_ => _.QuestionId));


            builder.HasOne(_ => _.UserAccount).WithMany(_ => _.Questions).HasForeignKey(_ => _.UserId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(_ => _.QuestionCategory).WithMany(_ => _.Questions).HasForeignKey(_ => _.QuestionCategoryId);
                

            
        }
    }
}
