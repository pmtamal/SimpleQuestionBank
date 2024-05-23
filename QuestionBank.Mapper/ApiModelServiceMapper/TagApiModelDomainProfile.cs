using QuestionBank.Model.Domain;

namespace QuestionBank.Mapper.ApiModelServiceMapper;

public  partial class ApiModelServiceProfile
{
    public void AddTagApiModelDomainProfile()
    {
        CreateMap<Model.Api.SkillsTagApiModel, SkillsTag>();


        CreateMap<SkillsTag, Model.Api.SkillsTagApiModel>();
    }
    
}