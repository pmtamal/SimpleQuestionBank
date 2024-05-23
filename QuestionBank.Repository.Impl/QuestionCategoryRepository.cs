using Microsoft.EntityFrameworkCore;
using QuestionBank.Persistence.Entity;
using QuestionBank.Repository.Interface;

namespace QuestionBank.Repository.Impl
{
    public class QuestionCategoryRepository : Repository<QuestionCategory, long>, IQuestionCategoryRepository
    {
        public QuestionCategoryRepository(DbContext context) : base(context)
        {
                      

        }

        public async Task AddQuestionCatagoryUserActions(IEnumerable<QuestionCategoryUserAction> questionCategoryUserActions)
        {
            await GetEntity<QuestionCategoryUserAction>().AddRangeAsync(questionCategoryUserActions);
        }

        public async Task DeleteAllUserActionFromCategory(long questionCategoryId)
        {
            await GetEntity<QuestionCategoryUserAction>().Where(_ => _.QuestionCategoryId == questionCategoryId).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<QuestionCategory>> GetAllCategoryWithUserInfo()
        {
            return await Data.Include(_=>_.UserAccounts).ThenInclude(uac=>uac.Person).ToListAsync();
        }

        public async Task<QuestionCategory> GetCategoryWithUserInfo(long categoryId)
        {
            return await Data.Include(_ => _.UserAccounts).Include(_ => _.QuestionCategoryUserActions)
                             .Where(_ => _.Id == categoryId)
                             .FirstOrDefaultAsync();
        }
    }
}
