using Microsoft.EntityFrameworkCore;
using QuestionBank.Persistence.Entity;
using QuestionBank.Repository.Interface;

namespace QuestionBank.Repository.Impl
{
    public class UserAccountRepository : Repository<UserAccount, long>, IUserAccountRepository
    {
        public UserAccountRepository(DbContext context) : base(context)
        {


        }

        public async Task<UserAccount> GetByEmailAsync(string email)
        {
            return await SingleOrDefaultAsync(_=>_.Email == email);
        }

        public async Task DeleteAllTagsFromUserAsync(long userId)
        {
            await GetEntity<UserTag>().Where(_ => _.UserId == userId).ExecuteDeleteAsync();
        }
        public async Task AddUserTagsAsync(IEnumerable<UserTag> userTags)
        {
            await GetEntity<UserTag>().AddRangeAsync(userTags);
        }

    }
}
