using QuestionBank.Model.Domain;

namespace QuestionBank.Service.Interface
{
    public interface ITagService:IBaseService
    {
        Task<IEnumerable<SkillsTag>> GetSkillsTagsAsync();
    }
}
