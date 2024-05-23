namespace QuestionBank.Model.Api
{
    public class QuestionCategoryApiModel
    {
        public long Id { get; set; }

        public required string Title { get; set; }
        public required string Description { get; set; }
        public required int MinNoOfReviewers { get; set; }

        public List<long> ReviewerUesrIds { get; set; }
        public List<long> ApproverUesrIds { get; set; }

        public List<UserInfoApiModel>? ReviewerUesrs { get; set; }
        public List<UserInfoApiModel>? ApprovalUesrs { get; set; }

    }
}
