using QuestionBank.Model.Domain;


namespace QuestionBank.Mapper.DomainEntityMapper;

public  partial class DomainEntityProfile
{
    public void AddQuestionDomainEntityProfile()
    {
        CreateMap<QuestionBank.Persistence.Entity.Question, Question>()
            .ForMember(_ => _.SkillsTagIds, _ => _.MapFrom(pm => pm.SkillsTags!=null?pm.SkillsTags.Select(_ => _.Id).ToList():new List<long>()))
            .ForMember(_ => _.CreatedBy, _ => _.MapFrom(pm => pm.UserAccount.Person.FullName));


        CreateMap<Question, QuestionBank.Persistence.Entity.Question>();
    }
    
}