using QuestionBank.Persistence.Entity;

namespace QuestionBank.Repository.Interface
{
    public interface IUserAccountRepository:IRepository<UserAccount,long>
    {
        Task<UserAccount> GetByEmailAsync(string email);
        Task DeleteAllTagsFromUserAsync(long userId);
        Task AddUserTagsAsync(IEnumerable<UserTag> userTags);
    }
}
