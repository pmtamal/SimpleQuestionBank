namespace QuestionBank.Persistence.Entity
{
    public class QuestionCategory
    {
        public long Id { get; set; }

        public required string Title { get; set; }
        public required string Description { get; set; }
        public required int MinNoOfReviewers { get; set; }

        public  ICollection<UserAccount> UserAccounts { get; set; }

        public ICollection<QuestionCategoryUserAction> QuestionCategoryUserActions { get;}
        
        public ICollection<Question> Questions { get;}
        
    }
}
