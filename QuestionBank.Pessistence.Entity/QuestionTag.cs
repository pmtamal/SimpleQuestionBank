namespace QuestionBank.Persistence.Entity
{
    public class QuestionTag
    {
        public int Id { get; set; }
        public  SkillsTag Tag { set; get; }
        public  Question Question { set; get; }
        public  long TagId { set; get; }
        
        public  long QuestionId{ set; get; }
    }
}
