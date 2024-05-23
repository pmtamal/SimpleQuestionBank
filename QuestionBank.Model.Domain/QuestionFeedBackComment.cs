using QuestionBank.Common.Enumeration;

namespace QuestionBank.Model.Domain
{
    public class QuestionFeedBackComment
    {
        
        public long Id { get; set; }
        public string Comment { set; get; }

        public long QuestionId { get; set; }

        public long UserId { set; get; }

        public string Commenter { set; get; }

        public DateTime CommentSubmittedOn { get; set; }

        public QuestionCommentStatus CommentStatus { get; set; }

        public bool IsEditableByUser {get; set; }

        public bool IsResolved { set; get; }
    }
}
