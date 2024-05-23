using QuestionBank.Common.Enumeration;
using QuestionBank.Model.Domain;
namespace QuestionBank.Mapper.ApiModelServiceMapper;

public partial class ApiModelServiceProfile
{
    public void AddUserAccountApiModelDomainProfile()
    {

        CreateMap<Model.Api.UserAccountApiModel, UserAccount>();
        
        
        CreateMap<UserAccount, Model.Api.UserAccountApiModel>();

        

        CreateMap<UserAccount, Model.Api.UserInfoApiModel>()
            .ForMember(_ => _.FullName, _ => _.MapFrom(dm => dm.Person.FullName))
            .ForMember(_ => _.Role, _ => _.MapFrom(dm => ((UserRole)dm.RoleId).ToString()))
            .ForMember(_=>_.SkillsTag,_=>_.MapFrom(dm=>dm.SkillsTags!=null?string.Join(", ",dm.SkillsTags.Select(st=>st.Name)):""));
    }
}