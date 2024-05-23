using QuestionBank.Common.Enumeration;

namespace QuestionBank.Model.Api
{
    public class QuestionViewApiModel
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        
        public string SampleAnswer { get; set; }

        

        public string CategoryName { get; set; }
        public string Tags { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? FinalizedOn { set; get; }

        public DateTime CreatedOn { set; get; }

        public DateTime? LastUpdatedOn { set; get; }

        public QuestionStatus Status { get; set; }


    }
}
