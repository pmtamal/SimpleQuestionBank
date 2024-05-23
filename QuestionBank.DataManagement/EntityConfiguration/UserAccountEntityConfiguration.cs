using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestionBank.Persistence.Entity;

namespace QuestionBank.DataManagement.EntityConfiguration;

public class UserAccountEntityConfiguration : IEntityTypeConfiguration<UserAccount>
{
    public void Configure(EntityTypeBuilder<UserAccount> builder)
    {
        builder.ToTable("UserAccount");
        builder.Property(userAccount => userAccount.AccountStatus).IsRequired();

        builder.Property(userAccount => userAccount.Email).IsRequired().HasMaxLength(255);
        builder.HasIndex(userAccount => userAccount.Email).IsUnique();

        builder.Property(userAccount => userAccount.CellNo).HasMaxLength(255);

        builder.Property(userAccount => userAccount.RegisteredOn).IsRequired();

        builder.Property(userAccount => userAccount.Password).IsRequired().HasMaxLength(2048);


        builder.HasOne(uac => uac.Role)
            .WithMany(role => role.UserAccounts);


        builder.HasOne(uac => uac.Person)
            .WithMany(role => role.UserAccounts)
            .OnDelete(DeleteBehavior.Cascade);



        builder.HasMany(_ => _.QuestionCategories).WithMany(_ => _.UserAccounts).UsingEntity<QuestionCategoryUserAction>
                (l => l.HasOne(_ => _.QuestionCategory).WithMany(_ => _.QuestionCategoryUserActions).HasForeignKey(_ => _.QuestionCategoryId), r => r.HasOne(_ => _.User).WithMany(_ => _.QuestionCategoryUserActions).HasForeignKey(_ => _.UserId));


        builder.HasMany(_ => _.SkillsTags).WithMany(_ => _.UserAccounts).UsingEntity<UserTag>
            (l => l.HasOne(_ => _.Tag).WithMany(_ => _.UserTags).HasForeignKey(_ => _.TagId),
            r => r.HasOne(_ => _.User).WithMany(_ => _.UserTags).HasForeignKey(_ => _.UserId));








    }
}