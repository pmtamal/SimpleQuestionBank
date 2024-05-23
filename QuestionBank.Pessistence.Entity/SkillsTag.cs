namespace QuestionBank.Persistence.Entity
{
    public class SkillsTag
    {
        public long Id { get; set; }

        public required string Name { get; set; }
        public required string Description { get; set; }

        public ICollection<Question>  Questions { get; set; }
        
        public ICollection<UserAccount>  UserAccounts { get; set; }
        public ICollection<UserTag>  UserTags { get; set; }
    }
}
