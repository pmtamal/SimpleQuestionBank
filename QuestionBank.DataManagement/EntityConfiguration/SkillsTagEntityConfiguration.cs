using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestionBank.Persistence.Entity;

namespace QuestionBank.DataManagement.EntityConfiguration;

public class SkillsTagEntityConfiguration : IEntityTypeConfiguration<SkillsTag>
{
    public void Configure(EntityTypeBuilder<SkillsTag> builder)
    {

        builder.ToTable("SkillsTag");

        builder.Property(_ => _.Name).HasMaxLength(100);
        
        builder.Property(role => role.Description).IsRequired(false).HasMaxLength(1000);

        


    }
}