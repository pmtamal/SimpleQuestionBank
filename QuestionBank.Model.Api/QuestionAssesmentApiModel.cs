using QuestionBank.Common.Enumeration;

namespace QuestionBank.Model.Api
{
    public class QuestionAssesmentApiModel
    {
        public long UserId { get; set; }

        public long QuestionId { get; set; }


        public List<QuestionFeedBackCommentApiModel> QuestionFeedBackComments { get; set; }

        public QuestionCommentStatus CommentStatus { get; set; }


    }
}
