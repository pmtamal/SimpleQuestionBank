using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestionBank.Persistence.Entity;

namespace QuestionBank.DataManagement.EntityConfiguration
{
    internal class QuestionFeedBackEntityConfiguration : IEntityTypeConfiguration<QuestionFeedBack>
    {
        public void Configure(EntityTypeBuilder<QuestionFeedBack> builder)
        {
            builder.ToTable("QuestionFeedBack");

            builder.Property(_ => _.UserId).IsRequired();
            
            builder.Property(_ => _.QuestionId).IsRequired();
            
            builder.Property(_ => _.FeedBackType).IsRequired();
            
            builder.Property(_ => _.LastFeedBackSubmittedOn).IsRequired();

            builder.HasOne(_=>_.Question).WithMany(_=>_.QuestionFeedBacks).HasForeignKey(_ => _.QuestionId);
            
            builder.HasOne(_=>_.User).WithMany(_=>_.QuestionFeedBacks).HasForeignKey(_ => _.UserId);

            


        }
    }
}
