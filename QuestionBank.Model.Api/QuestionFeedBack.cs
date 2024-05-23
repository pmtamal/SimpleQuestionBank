using QuestionBank.Common.Enumeration;

namespace QuestionBank.Model.Api
{
    public class QuestionFeedBackApiModel
    {
        public long Id { get; set; }

        public long QuestionId { get; set; }
        public QuestionApiModel? Question { get; set; }
        public long UserId { get; set; }

        public UserAccountApiModel? User { get; set; }

        public DateTime FeedBackSubmittedOn { get; set; }

        public QuestionFeedBackType FeedBackType { get; set; }

        


    }
}
