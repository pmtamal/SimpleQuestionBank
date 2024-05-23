using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestionBank.Persistence.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.DataManagement.EntityConfiguration
{
    public class QuestionFeedBackCommentEntityConfiguration : IEntityTypeConfiguration<QuestionFeedBackComment>
    {
        public void Configure(EntityTypeBuilder<QuestionFeedBackComment> builder)
        {
            builder.ToTable("QuestionFeedBackComment"); 

            builder.Property(_ => _.UserId).IsRequired();

            builder.Property(_ => _.QuestionId).IsRequired();

            builder.Property(_ => _.CommentStatus).IsRequired();

            builder.Property(_ => _.CommentSubmittedOn).IsRequired();
            
            builder.Property(_ => _.Comment).HasMaxLength(5000);

            builder.HasOne(_ => _.Question).WithMany(_ => _.questionFeedBackComments).HasForeignKey(_ => _.QuestionId);

            builder.HasOne(_ => _.User).WithMany(_ => _.QuestionFeedBackComments).HasForeignKey(_ => _.UserId);
        }
    }
}
