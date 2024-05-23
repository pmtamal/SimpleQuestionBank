using QuestionBank.Model.Domain;

namespace QuestionBank.Mapper.ApiModelServiceMapper;

public  partial class ApiModelServiceProfile
{
    public void AddPersonApiModelDomainProfile()
    {
        CreateMap<Model.Api.PersonApiModel, Person>();


        CreateMap<Person, Model.Api.PersonApiModel>();
        
        CreateMap<ServiceResult, Model.Api.ResponseBase>()
            .ForMember(_=>_.ErrorCode,_=>_.MapFrom(dm=>(int)dm.ErrorCode));
    }
    
}