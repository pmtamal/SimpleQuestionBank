using QuestionBank.Common.Enumeration;
using QuestionBank.Model.Api;
using QuestionBank.Model.Domain;

namespace QuestionBank.Mapper.ApiModelServiceMapper;

public  partial class ApiModelServiceProfile
{
    public void AddQuestionApiModelDomainProfile()
    {
        CreateMap<Model.Api.QuestionApiModel, Question>()
            .ForMember(_ => _.Status, _ => _.MapFrom(apm => apm.SaveAsDraft ? QuestionStatus.Draft : QuestionStatus.InReview));


        CreateMap<Question, Model.Api.QuestionApiModel>()
            .ForMember(_ => _.SkillsTags, _ => _.MapFrom((src, dest, destMember, context) => src.SkillsTags?.Select(context.Mapper.Map<SkillsTagApiModel>)));


        CreateMap<Question, Model.Api.QustionTableApiModel>()
            .ForMember(_ => _.CategoryName, _ => _.MapFrom(dm => dm.QuestionCategory.Title))
            .ForMember(_ => _.Tags, _ => _.MapFrom(dm => dm.SkillsTags != null ? string.Join(",", dm.SkillsTags.Select(_ => _.Name)) : ""));
            

        CreateMap<Question, Model.Api.QuestionViewApiModel>()
            .ForMember(_ => _.CategoryName, _ => _.MapFrom(dm => dm.QuestionCategory.Title))
            .ForMember(_ => _.Tags, _ => _.MapFrom(dm => dm.SkillsTags != null ? string.Join(",", dm.SkillsTags.Select(_ => _.Name)) : ""));





    }
    
}