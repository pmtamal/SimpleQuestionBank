using QuestionBank.Common.Enumeration;

namespace QuestionBank.Persistence.Entity
{
    public class QuestionFeedBackComment
    {
        public long Id { get; set; }

        public long QuestionId { get; set; }
        public Question Question { get; set; }
        public long UserId { get; set; }

        public UserAccount User { get; set; }

        public DateTime CommentSubmittedOn { get; set; }

        public string Comment { get; set; }

        public QuestionCommentStatus CommentStatus { get; set; }
    }
}
