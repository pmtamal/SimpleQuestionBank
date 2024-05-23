using QuestionBank.Model.Domain;

namespace QuestionBank.Service.Interface
{
    public interface ISettingService:IBaseService
    {
        Task<IEnumerable<Role>> GetRoles();
    }
}
