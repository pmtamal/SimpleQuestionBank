using QuestionBank.Model.Domain;

namespace QuestionBank.Mapper.DomainEntityMapper;

public  partial class DomainEntityProfile
{
    public void AddSkillTagDomainEntityProfile()
    {
        CreateMap<Persistence.Entity.SkillsTag, SkillsTag>();


        CreateMap<SkillsTag, QuestionBank.Persistence.Entity.SkillsTag>();
    }
    
}