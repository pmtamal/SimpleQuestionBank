using QuestionBank.Model.Domain;


namespace QuestionBank.Mapper.DomainEntityMapper;

public  partial class DomainEntityProfile
{
    public void AddCategoryDomainEntityProfile()
    {
        CreateMap<QuestionCategory,Persistence.Entity.QuestionCategory>();
            

        CreateMap<Persistence.Entity.QuestionCategory, QuestionCategory>()
            .ForMember(_ => _.ApprovalUesrs, _ => _.MapFrom((src, dest, destmember, context) =>
            src.QuestionCategoryUserActions?.Where(_ => _.CategoryUserAction == Common.Enumeration.CategoryUserAction.Approve).Select(_ => context.Mapper.Map<UserAccount>(_.User))))
            .ForMember(_ => _.ReviewerUesrs, _ => _.MapFrom((src, dest, destmember, context) =>
            src.QuestionCategoryUserActions?.Where(_ => _.CategoryUserAction == Common.Enumeration.CategoryUserAction.Review).Select(_ => context.Mapper.Map<UserAccount>(_.User))));
        
            
    }
    
}