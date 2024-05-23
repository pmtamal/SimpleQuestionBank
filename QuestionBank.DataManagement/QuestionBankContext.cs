using Microsoft.EntityFrameworkCore;
using QuestionBank.DataManagement.EntityConfiguration;
using QuestionBank.Persistence.Entity;

namespace QuestionBank.DataManagement;

public class QuestionBankContext : DbContext
{
    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<UserTag> UserTags { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    public DbSet<QuestionCategory> QuestionCategories { get; set; }
    public DbSet<QuestionCategoryUserAction> QuestionCategoryUserActions { get; set; }

    public DbSet<SkillsTag> SkillsTags { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionTag> QuestionTags { get; set; }
    public DbSet<QuestionFeedBack> QuestionFeedBacks  { get; set; }
    public DbSet<QuestionFeedBackComment> QuestionFeedBackComments  { get; set; }


    public QuestionBankContext(DbContextOptions options) : base(options)
    {

    }

    public QuestionBankContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<FakeEntity>();

        modelBuilder.ApplyConfiguration(new UserAccountEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PersonEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionCategoryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionCategoryUserActionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionTagEntityConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionFeedBackEntityConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionFeedBackCommentEntityConfiguration());
        modelBuilder.ApplyConfiguration(new SkillsTagEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserTagEntityConfiguration());
    }
}