using QuestionBank.Common.Enumeration;
using QuestionBank.Persistence.Entity;

namespace QuestionBank.Repository.Interface
{
    public interface IQuestionFeedBackRepository : IRepository<QuestionFeedBack, long>
    {
        Task AddQuestionFeedBackComment(QuestionFeedBackComment questionFeedBackComments);
        Task AddQuestionFeedBackComments(IEnumerable<QuestionFeedBackComment> questionFeedBackComments);
        Task<bool> AreCommentsStillUnResolved(long questionId);
        Task CompleteFeedbackCycle(long questionId);
        Task DeleteQuestionFeedBackComment(long id,long userId);
        Task<IEnumerable<Question>> GetAllQuestionForReviewByStatusAndUserId(ReviewStatus reviewStatus, long userId, QuestionStatus questionStatus, CategoryUserAction categoryUserAction);
        Task<QuestionFeedBackComment> GetQuestionFeedBackCommentAsync(long id);
        Task<IEnumerable<QuestionFeedBackComment>> GetQuestionFeedBackCommentsByQuestionId(long userId, long questionId);
        Task<IEnumerable<QuestionFeedBackComment>> GetQuestionFeedBackCommentsByUserAsync(long userId, long questionId);
    }
}
