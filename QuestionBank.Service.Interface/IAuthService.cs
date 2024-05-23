using QuestionBank.Model.Domain;

namespace QuestionBank.Service.Interface
{
    public interface IAuthService
    {
        Task AddRefreshToken(UserRefreshToken userRefreshToken);
        Task<UserAccount?> AuthenticateUserAsync(string userName, string password);
        Task DeleteRfreshToken(string refreshToken);
        Task<UserRefreshToken> GetRefreshToken(string refreshToken);
        
    }
}
