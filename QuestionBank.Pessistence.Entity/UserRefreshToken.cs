namespace QuestionBank.Persistence.Entity
{
    public class UserRefreshToken
    {
        public long Id { get; set; }

        public required string UserName { get; set;}
        public required string RefreshToken { get; set;}
        public bool Active { get; set; } = true;
    }
}
