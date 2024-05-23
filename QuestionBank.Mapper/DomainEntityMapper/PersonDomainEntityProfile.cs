using QuestionBank.Model.Domain;


namespace QuestionBank.Mapper.DomainEntityMapper;

public  partial class DomainEntityProfile
{
    public void AddPersonDomainEntityProfile()
    {
        CreateMap<QuestionBank.Persistence.Entity.Person, Person>();


        CreateMap<Person, QuestionBank.Persistence.Entity.Person>();
    }
    
}