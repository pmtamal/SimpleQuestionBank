using Microsoft.EntityFrameworkCore;
using QuestionBank.Persistence.Entity;
using QuestionBank.Repository.Interface;

namespace QuestionBank.Repository.Impl
{
    public class MiscRepository : Repository<FakeEntity, long>, IMiscRepository
    {
        public MiscRepository(DbContext context) : base(context)
        {
        }

        public async Task AddRefreshTokenAsync(UserRefreshToken userRefreshToken)
        {
            await GetEntity<UserRefreshToken>().AddAsync(userRefreshToken);
        }

        public async Task DeleteRefreshTokenAsync(string refreshToken)
        {
            
            await GetEntity<UserRefreshToken>().Where(_=>_.RefreshToken==refreshToken).ExecuteDeleteAsync();
            

        }

        public async Task<IEnumerable<SkillsTag>> GetAllSkillsTag()
        {
            return await Task.Run(()=> GetEntity<SkillsTag>());

        }

        public async Task<UserRefreshToken> GetRefreshTokenAsync(string refreshToken)
        {
            return await GetEntity<UserRefreshToken>().FirstOrDefaultAsync(_ => _.RefreshToken == refreshToken);
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            var roles = await GetEntity<Role>().ToListAsync();
            //var roles =  await Task.Run(() => GetEntity<Role>());
            return roles;
        }
    }
}
