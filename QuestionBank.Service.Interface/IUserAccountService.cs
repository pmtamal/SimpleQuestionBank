using QuestionBank.Model.Domain;

namespace QuestionBank.Service.Interface
{
    public interface IUserAccountService:IBaseService
    {
        Task RegisterNewUserAsync(UserAccount userAccount);
        Task<UserAccount> GetUserInfoByIdAsync(long id);
        Task<UserAccount> GetUserByIdAsync(long id);
        Task<IEnumerable<UserAccount>> GetAllReviewers();
        Task<IEnumerable<UserAccount>> GetAllApprovers();
        Task<IEnumerable<UserAccount>> GetAllUsersAsync();
        Task UpdateUserAsync(UserAccount userAccount);
        Task<bool> ResetUserPasswordAsync(long userId, string currentPassword, string newPassword);

        Task UploadUserImage(long userId, string image);

        Task<string?> GetUserImage(long userId);
    }
}
