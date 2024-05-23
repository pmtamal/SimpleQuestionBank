using Microsoft.EntityFrameworkCore;
using QuestionBank.Common.Enumeration;
using QuestionBank.Persistence.Entity;
using QuestionBank.Repository.Interface;

namespace QuestionBank.Repository.Impl
{
    public class QuestionRepository : Repository<Question, long>, IQuestionRepository
    {
        public QuestionRepository(DbContext context) : base(context)
        {

        }

        public async Task AddQuestionTags(IEnumerable<QuestionTag> questionTags)
        {

            await GetEntity<QuestionTag>().AddRangeAsync(questionTags);
        }

        public async Task DeleteAllTagsFromQuestion(long questionId)
        {
            await GetEntity<QuestionTag>().Where(_ => _.QuestionId == questionId).ExecuteDeleteAsync();
        }

        public async Task<Question> GetQuestionWithTags(long questionId)
        {
            return await Data.Include(_ => _.SkillsTags).Include(_=>_.QuestionCategory).Where(_ => _.Id == questionId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Question>> GetAllQuestionByStatusWithUserInfo(QuestionStatus questionStatus)
        {
            return await Data.Include(_ => _.QuestionCategory).Include(_ => _.SkillsTags).Include(_ => _.UserAccount).ThenInclude(_ => _.Person).Where(_ => _.status == questionStatus).ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetAllQuestionByStatusAndUserId(QuestionStatus questionStatus,long userId)
        {
            return await Data.Include(_ => _.QuestionCategory).Include(_ => _.SkillsTags).Include(_ => _.UserAccount).ThenInclude(_ => _.Person).
                Where(_ =>(questionStatus== QuestionStatus.All ||  _.status == questionStatus)&&_.UserId==userId).ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetAllReviewQuestionByStatusAndUserId(ReviewStatus reviewStatus, long userId)
        {
            return await Data.Include(_ => _.QuestionCategory).Include(_ => _.SkillsTags).Include(_ => _.UserAccount).ThenInclude(_ => _.Person).
                Where(_ => (_.UserId == userId)).ToListAsync();
        }
    }
}
