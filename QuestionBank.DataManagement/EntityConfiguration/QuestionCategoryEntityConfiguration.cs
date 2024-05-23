using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestionBank.Persistence.Entity;

namespace QuestionBank.DataManagement.EntityConfiguration
{
    internal class QuestionCategoryEntityConfiguration : IEntityTypeConfiguration<QuestionCategory>
    {
        public void Configure(EntityTypeBuilder<QuestionCategory> builder)
        {
            builder.ToTable(nameof(QuestionCategory));
            builder.Property(_ => _.Title).IsRequired().HasMaxLength(100);
            builder.Property(_ => _.Description).HasMaxLength(500).IsRequired(false);
            builder.Property(_ => _.MinNoOfReviewers).IsRequired();

            builder.HasMany(_ => _.UserAccounts).WithMany(_ => _.QuestionCategories).UsingEntity<QuestionCategoryUserAction>
                (l=>l.HasOne(_=>_.User).WithMany(_=>_.QuestionCategoryUserActions).HasForeignKey(_=>_.UserId),r=>r.HasOne(_=>_.QuestionCategory).WithMany(_=>_.QuestionCategoryUserActions).HasForeignKey(_=>_.QuestionCategoryId));

        }
    }
}
