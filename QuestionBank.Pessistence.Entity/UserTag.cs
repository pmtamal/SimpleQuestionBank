namespace QuestionBank.Persistence.Entity
{
    public class UserTag
    {
        public long Id { get; set; }
        public  SkillsTag Tag { set; get; }
        public  UserAccount User { set; get; }
        public  long TagId { set; get; }
        
        public  long UserId{ set; get; }
    }
}
