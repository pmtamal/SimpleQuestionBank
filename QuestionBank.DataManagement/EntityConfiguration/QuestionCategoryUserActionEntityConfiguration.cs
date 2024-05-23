using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestionBank.Persistence.Entity;

namespace QuestionBank.DataManagement.EntityConfiguration
{
    internal class QuestionCategoryUserActionEntityConfiguration : IEntityTypeConfiguration<QuestionCategoryUserAction>
    {
        public void Configure(EntityTypeBuilder<QuestionCategoryUserAction> builder)
        {
            builder.ToTable("QuestionCategoryUserAction");


        }
    }
}
