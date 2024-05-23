using QuestionBank.Model.Domain;

namespace QuestionBank.Service.Interface
{
    public interface IQuestionCategoryService:IBaseService
    {
        Task AddCatagory(QuestionCategory questionCategory);
        Task UpdateCategory(QuestionCategory questionCategory);
        Task<IEnumerable<QuestionCategory>> GetAllQuestionCategory();

        Task<QuestionCategory> GetQuestionCategoryById(long questionCategoryId);

        Task<IEnumerable<SkillsTag>> GetQuestionSkills();


        


    }
}
