using AutoMapper;

namespace QuestionBank.Mapper.DomainEntityMapper;

public partial class DomainEntityProfile :Profile
{
    public DomainEntityProfile()
    {

        AddPersonDomainEntityProfile();
        AddUserAccountDomainEntityProfile();
        AddUserRefreshTokenomainEntityProfile();
        AddCategoryDomainEntityProfile();
        AddSkillTagDomainEntityProfile();
        AddQuestionDomainEntityProfile();
        AddQuestionFeddBackDomainEntityProfile();
        RoleDomainEntityProfile();
    }
}