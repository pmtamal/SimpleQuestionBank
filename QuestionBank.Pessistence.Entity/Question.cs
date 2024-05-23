using QuestionBank.Common.Enumeration;

namespace QuestionBank.Persistence.Entity
{
    public class Question
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string SampleAnswer { set; get; }

        public long QuestionCategoryId { get; set; }
        public QuestionCategory QuestionCategory { get; set; }
        
        public ICollection<SkillsTag> SkillsTags { get; set; }
        
        public ICollection<QuestionTag> QuestionTags { get; set; }
        
        public ICollection<QuestionFeedBack> QuestionFeedBacks { get; set; }
        public ICollection<QuestionFeedBackComment> questionFeedBackComments { get; set; }

        public QuestionStatus status { get; set; }

        public long UserId { get; set; }
        public UserAccount UserAccount { get; set; }

        public DateTime? FinalizedOn { set; get; }

        public DateTime CreatedOn { set; get; }

        public DateTime LastUpdatedOn { set; get;}
        
        



    }
}
