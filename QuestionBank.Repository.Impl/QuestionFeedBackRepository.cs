using Microsoft.EntityFrameworkCore;
using QuestionBank.Common.Enumeration;
using QuestionBank.Persistence.Entity;
using QuestionBank.Repository.Interface;

namespace QuestionBank.Repository.Impl
{
    public class QuestionFeedBackRepository : Repository<QuestionFeedBack, long>, IQuestionFeedBackRepository
    {
        public QuestionFeedBackRepository(DbContext context) : base(context)
        {

        }

        public async Task AddQuestionFeedBackComments(IEnumerable<QuestionFeedBackComment> questionFeedBackComments) {

            await GetEntity<QuestionFeedBackComment>().AddRangeAsync(questionFeedBackComments);        
        
        }

        public async Task<IEnumerable<QuestionFeedBackComment>> GetQuestionFeedBackCommentsByUserAsync(long  userId,long questionId)
        {

            return await GetEntity<QuestionFeedBackComment>().Include(_=>_.User).ThenInclude(_=>_.Person).Where(_=>_.UserId==userId&&_.QuestionId==questionId).ToListAsync();

        }
        public async Task<IEnumerable<QuestionFeedBackComment>> GetQuestionFeedBackCommentsByQuestionId(long  userId,long questionId)
        {

            return await GetEntity<QuestionFeedBackComment>().Include(_=>_.User).ThenInclude(_=>_.Person).Where(_=>_.QuestionId==questionId).ToListAsync();

        }

        public async Task AddQuestionFeedBackComment(QuestionFeedBackComment questionFeedBackComments)
        {

            await GetEntity<QuestionFeedBackComment>().AddAsync(questionFeedBackComments);

        }

        public async Task<QuestionFeedBackComment> GetQuestionFeedBackCommentAsync(long id)
        {

            return await GetEntity<QuestionFeedBackComment>().Include(_=>_.User).ThenInclude(_=>_.Person).Where(_=>_.Id==id).FirstOrDefaultAsync();

        }

        public async Task DeleteQuestionFeedBackComment(long id,long userId)
        {

           await  GetEntity<QuestionFeedBackComment>().Where(_=>_.Id==id&&_.UserId==userId).ExecuteDeleteAsync();

        }

        public async Task<IEnumerable<Question>> GetAllQuestionForReviewByStatusAndUserId(ReviewStatus reviewStatus, long userId,QuestionStatus questionStatus, CategoryUserAction categoryUserAction) {

            return await GetEntity<Question>().Include(_ => _.QuestionFeedBacks).Include(_=>_.SkillsTags).Include(_ => _.QuestionCategory).ThenInclude(_ => _.QuestionCategoryUserActions).Include(_=>_.UserAccount).ThenInclude(_=>_.Person).
                Where(_ => _.QuestionCategory.QuestionCategoryUserActions.
                Any(uc => uc.UserId == userId && uc.CategoryUserAction == categoryUserAction)
                &&  
                
                ( ((reviewStatus == ReviewStatus.All ||reviewStatus == ReviewStatus.Pending) &&_.status == questionStatus && (_.QuestionFeedBacks == null||_.QuestionFeedBacks.All(qf=>qf.UserId!=userId&&!qf.IsFeedBackCycleCompleted))) 
                || ((reviewStatus == ReviewStatus.All || reviewStatus == ReviewStatus.Completed) && _.QuestionFeedBacks.Any(qf=>qf.UserId==userId)))).ToListAsync();
        
        }

        public async Task<bool> AreCommentsStillUnResolved(long questionId)
        {
            return await GetEntity<QuestionFeedBackComment>().AnyAsync(_ => _.QuestionId == questionId && _.CommentStatus == QuestionCommentStatus.WaitingForResolve);

        }

        public async Task CompleteFeedbackCycle(long questionId)
        {
            await Data.Where(_=>_.QuestionId==questionId).ExecuteUpdateAsync(setters=>setters.SetProperty(p=>p.IsFeedBackCycleCompleted,true));
        }
    }
}
