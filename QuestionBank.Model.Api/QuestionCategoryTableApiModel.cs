namespace QuestionBank.Model.Api
{
    public class QuestionCategoryTableApiModel
    {
        public long Id { get; set; }

        public required string Title { get; set; }
        public required string Description { get; set; }
        public required int MinNoOfReviewers { get; set; }


        public string Reviewers { get; set; }
        public string Approvers { get; set; }

    }
}
