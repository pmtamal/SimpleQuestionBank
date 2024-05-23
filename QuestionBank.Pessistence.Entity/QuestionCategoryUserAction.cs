using QuestionBank.Common.Enumeration;

namespace QuestionBank.Persistence.Entity
{
    public class QuestionCategoryUserAction
    {
        public long Id { get; set; }

        public long QuestionCategoryId { get; set; }
        public QuestionCategory QuestionCategory { get; set; }

        public long UserId { get; set; }
        public UserAccount User { get; set; }

        public CategoryUserAction CategoryUserAction { get; set; }
    }
}
