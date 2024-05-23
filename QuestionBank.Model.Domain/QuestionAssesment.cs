using QuestionBank.Common.Enumeration;

namespace QuestionBank.Model.Domain
{
    public class QuestionAssesment
    {
        public long UserId { get; set; }

        public long QuestionId { get; set; }


        public List<QuestionFeedBackComment> QuestionFeedBackComments { get; set; }

        public QuestionCommentStatus CommentStatus { get; set; }


    }
}
