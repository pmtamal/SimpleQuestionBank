using QuestionBank.Common.Enumeration;

namespace QuestionBank.Model.Api
{
    public class QustionTableApiModel
    {
        public long Id { get; set; }

        public string Title { get; set; }

        

        public string CategoryName { get; set; }
        public string Tags { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? FinalizedOn { set; get; }

        public DateTime CreatedOn { set; get; }

        public DateTime? LastUpdatedOn { set; get; }

        public QuestionStatus Status { get; set; }


    }
}
