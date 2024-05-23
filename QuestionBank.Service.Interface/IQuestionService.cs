using QuestionBank.Common.Enumeration;
using QuestionBank.Model.Domain;

namespace QuestionBank.Service.Interface
{
    public interface IQuestionService:IBaseService
    {
        Task AddNewQuestion(Question questionBank);
        Task UpdateQuestion(Question questionBank);

        Task<Question> GetQuestion(long questionId);
        Task<IEnumerable<Question>> GetAllMergedQuestion();
        Task<IEnumerable<Question>> GetAllQuestionByStatusAndUserId(QuestionStatus questionStatus, long userId);
    }
}
