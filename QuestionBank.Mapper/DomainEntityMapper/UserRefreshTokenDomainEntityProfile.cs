using QuestionBank.Model.Domain;


namespace QuestionBank.Mapper.DomainEntityMapper;

public  partial class DomainEntityProfile
{
    public void AddUserRefreshTokenomainEntityProfile()
    {
        CreateMap<QuestionBank.Persistence.Entity.UserRefreshToken, UserRefreshToken>();


        CreateMap<UserRefreshToken, QuestionBank.Persistence.Entity.UserRefreshToken>();
    }
    
}