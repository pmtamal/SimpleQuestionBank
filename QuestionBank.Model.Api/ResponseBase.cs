namespace QuestionBank.Model.Api;

public class ResponseBase
{
    public bool HasError { get; set; }

    public string? ErrorMessage { get; set; }
    public int  ErrorCode { get; set; }
}