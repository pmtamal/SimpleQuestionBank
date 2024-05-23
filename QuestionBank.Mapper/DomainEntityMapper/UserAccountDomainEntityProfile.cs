using QuestionBank.Model.Domain;

namespace QuestionBank.Mapper.DomainEntityMapper;

public partial class DomainEntityProfile
{
    public void AddUserAccountDomainEntityProfile()
    {

        CreateMap<Persistence.Entity.UserAccount, UserAccount>()
        .ForMember(_ => _.Password, _ => _.Ignore());

        CreateMap<UserAccount, Persistence.Entity.UserAccount>()
            .ForMember(_ => _.Password, _ => _.Ignore());

    }
}