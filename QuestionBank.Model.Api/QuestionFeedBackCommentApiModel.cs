namespace QuestionBank.Model.Api
{
    public class QuestionFeedBackCommentApiModel
    {
        public long Id { get; set; }
        public string? Comment { set; get; }  
        public long QuestionId { set; get; }

        public string? commenter { set; get; }

        public DateTime commentDate { set; get; }

        public bool IsEditableByUser { set; get; }

        public bool IsResolved { set; get; }
        
    }
}
