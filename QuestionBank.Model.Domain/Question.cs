using QuestionBank.Common.Enumeration;

namespace QuestionBank.Model.Domain
{
    public class Question
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string SampleAnswer { set; get; }


        public long QuestionCategoryId { get; set; }
        public QuestionCategory QuestionCategory {   get; set; }
        public List<SkillsTag> SkillsTags { get; set; }
        public List<long> SkillsTagIds { get; set; }

        public QuestionStatus Status { get; set; }

        public DateTime? FinalizedOn { set; get; }

        public DateTime CreatedOn { set; get; }

        public DateTime? LastUpdatedOn { set; get; }

        public long UserId { set; get; }

        public string CreatedBy { set; get; }


    }
}
