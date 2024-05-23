namespace QuestionBank.Model.Domain
{
    public class UserRefreshToken
    {
        

        public required string UserName { get; set; }
        public required string RefreshToken { get; set; }
        
    }
}
