using QuestionBank.Common.Enumeration;
using QuestionBank.Model.Domain;

namespace QuestionBank.Service.Interface
{
    public interface IQuestionFeedBackService:IBaseService
    {
        Task AddQuestionFeedBack(QuestionFeedBack questionFeedBack);

        Task UpdateOrInsertQuestionFeedBack(QuestionFeedBack questionFeedBack);
        Task<QuestionFeedBackComment> AddFeedBackComment(QuestionFeedBackComment questionFeedBack);
        Task DeleteFeedBackComment(long id, long userId);
        Task<IEnumerable<QuestionFeedBackComment>> GetFeedBackCommentsByUser(long userId, long questionId);
        Task<IEnumerable<Question>> GetAllQuestionForReviewByStatusAndUserId(ReviewStatus reviewStatus, long v);
        Task UpdateFeedBackComment(QuestionFeedBackComment questionFeedBackComment);
        Task<IEnumerable<Question>> GetAllQuestionForApproverByStatusAndUserId(ReviewStatus reviewStatus, long userId);
        Task ResolveComment(long id);
    }
}
