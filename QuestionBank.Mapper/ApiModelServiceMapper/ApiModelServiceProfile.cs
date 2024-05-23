using AutoMapper;

namespace QuestionBank.Mapper.ApiModelServiceMapper;

public partial class ApiModelServiceProfile : Profile
{
    public ApiModelServiceProfile()
    {
        AddPersonApiModelDomainProfile();
        AddUserAccountApiModelDomainProfile();
        AddCategoryApiModelDomainProfile();
        AddQuestionApiModelDomainProfile();
        AddTagApiModelDomainProfile();
        AddQuestionFeddBackApiModelDomainProfile();

    }
}