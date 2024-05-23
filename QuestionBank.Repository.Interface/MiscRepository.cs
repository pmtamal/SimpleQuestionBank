using QuestionBank.Persistence.Entity;

namespace QuestionBank.Repository.Interface
{
    public interface IMiscRepository:IRepository<FakeEntity,long>
    {
        Task AddRefreshTokenAsync(UserRefreshToken userRefreshToken);
        Task DeleteRefreshTokenAsync(string refreshToken);

        Task<UserRefreshToken> GetRefreshTokenAsync(string refreshToken);

        Task<IEnumerable<SkillsTag>> GetAllSkillsTag();

        Task<IEnumerable<Role>> GetRoles();
    }
}
