using QuestionBank.Model.Domain;


namespace QuestionBank.Mapper.ApiModelServiceMapper;

public partial class ApiModelServiceProfile
{
    public void AddQuestionFeddBackApiModelDomainProfile()
    {
        CreateMap<Model.Api.QuestionFeedBackApiModel, QuestionFeedBack>();


        CreateMap<QuestionFeedBack, Model.Api.QuestionFeedBackApiModel>();

        CreateMap<Model.Api.QuestionFeedBackApiModel, QuestionFeedBack>();
        
        
        CreateMap<Question, Model.Api.QuestionReviewTableApiModel>()
            .ForMember(_ => _.CategoryName, _ => _.MapFrom(dm => dm.QuestionCategory.Title))
            .ForMember(_ => _.Tags, _ => _.MapFrom(dm => dm.SkillsTags != null ? string.Join(",", dm.SkillsTags.Select(_ => _.Name)) : ""));





        CreateMap<QuestionFeedBackComment, Model.Api.QuestionFeedBackCommentApiModel>()
            .ForMember(_ => _.commentDate, _ => _.MapFrom(dm => dm.CommentSubmittedOn))
            .ForMember(_ => _.IsResolved, _ => _.MapFrom(dm => dm.CommentStatus == Common.Enumeration.QuestionCommentStatus.Resolved));

            
        
        CreateMap<Model.Api.QuestionFeedBackCommentApiModel,QuestionFeedBackComment>();





    }

}