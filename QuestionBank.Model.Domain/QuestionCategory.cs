namespace QuestionBank.Model.Domain
{
    public class QuestionCategory
    {
        public long Id { get; set; }

        public required string Title { get; set; }
        public required string Description { get; set; }
        public required int MinNoOfReviewers { get; set; }

        public List<long> ReviewerUesrIds { get; set; }
        public List<long> ApproverUesrIds { get; set; }

        public List<UserAccount> ReviewerUesrs { get; set; }
        public List<UserAccount> ApprovalUesrs { get; set; }



    }
}
