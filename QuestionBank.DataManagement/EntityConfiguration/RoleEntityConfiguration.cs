using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestionBank.Persistence.Entity;

namespace QuestionBank.DataManagement.EntityConfiguration;

public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {

        builder.ToTable("Role");

        builder.Property(role => role.Name).IsRequired().HasMaxLength(255);
        
        builder.Property(role => role.Name).IsRequired().HasMaxLength(255);

        builder.Property(role => role.Description).HasMaxLength(4000);


    }
}