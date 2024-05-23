using QuestionBank.Common.Enumeration;
using QuestionBank.Persistence.Entity;

namespace QuestionBank.Repository.Interface
{
    public interface IQuestionRepository : IRepository<Question, long>
    {
        Task AddQuestionTags(IEnumerable<QuestionTag> questionTags);
        Task DeleteAllTagsFromQuestion(long questionId);
        Task<IEnumerable<Question>> GetAllQuestionByStatusAndUserId(QuestionStatus questionStatus, long userId);
        Task<IEnumerable<Question>> GetAllQuestionByStatusWithUserInfo(QuestionStatus questionStatus);
        Task<Question> GetQuestionWithTags(long questionId);
    }
}
