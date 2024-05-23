using QuestionBank.Common.Enumeration;

namespace QuestionBank.Model.Domain
{
    public class ServiceResult
    {
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        public ServiceError ErrorCode { get; set; }
    }
}
