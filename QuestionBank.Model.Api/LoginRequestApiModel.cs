namespace QuestionBank.Model.Api;

public class LoginRequestApiModel
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}