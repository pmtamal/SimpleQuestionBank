using QuestionBank.Model.Domain;


namespace QuestionBank.Mapper.DomainEntityMapper;

public  partial class DomainEntityProfile
{
    public void AddQuestionFeddBackDomainEntityProfile()
    {
        CreateMap<QuestionBank.Persistence.Entity.QuestionFeedBack, QuestionFeedBack>();


        CreateMap<QuestionFeedBack, QuestionBank.Persistence.Entity.QuestionFeedBack>();

        CreateMap<QuestionFeedBack, QuestionBank.Persistence.Entity.QuestionFeedBack>();

        CreateMap<QuestionFeedBackComment, QuestionBank.Persistence.Entity.QuestionFeedBackComment>();
        
        CreateMap<QuestionBank.Persistence.Entity.QuestionFeedBackComment, QuestionFeedBackComment>()
            .ForMember(_=>_.Commenter,_=>_.MapFrom(pm=>pm.User.Person.FullName));





    }
    
}