using QuestionBank.Model.Api;
using QuestionBank.Model.Domain;

namespace QuestionBank.Mapper.ApiModelServiceMapper;

public  partial class ApiModelServiceProfile
{
    public void AddCategoryApiModelDomainProfile()
    {
        CreateMap<Model.Api.QuestionCategoryApiModel, QuestionCategory>();

        


        CreateMap<QuestionCategory, Model.Api.QuestionCategoryApiModel>()
            .ForMember(_=>_.ReviewerUesrs,_=>_.MapFrom((src,dest,destMember,context)=>src.ReviewerUesrs.Select(context.Mapper.Map<UserInfoApiModel>)));

        CreateMap<QuestionCategory, Model.Api.QuestionCategoryApiModel>()
            .ForMember(_ => _.ApprovalUesrs, _ => _.MapFrom((src, dest, destMember, context) => src.ApprovalUesrs.Select(context.Mapper.Map<UserInfoApiModel>)));

        CreateMap<QuestionCategory, Model.Api.QuestionCategoryTableApiModel>()
            .ForMember(_ => _.Reviewers, _ => _.MapFrom(dm =>string.Join(",",dm.ReviewerUesrs.Select(ru => ru.Person.FullName))))
            .ForMember(_ => _.Approvers, _ => _.MapFrom(dm => string.Join(",", dm.ApprovalUesrs.Select(ru => ru.Person.FullName))));

            
    }
    
}