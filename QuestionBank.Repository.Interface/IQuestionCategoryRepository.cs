using QuestionBank.Persistence.Entity;

namespace QuestionBank.Repository.Interface
{
    public interface IQuestionCategoryRepository:IRepository<QuestionCategory,long>
    {
        Task AddQuestionCatagoryUserActions(IEnumerable<QuestionCategoryUserAction> questionCategoryUserActions);
        
        Task DeleteAllUserActionFromCategory(long categoryId);

        Task<QuestionCategory> GetCategoryWithUserInfo(long categoryId);

        
        Task<IEnumerable<QuestionCategory>> GetAllCategoryWithUserInfo();
    }
}
