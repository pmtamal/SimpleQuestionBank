namespace QuestionBank.Model.Api
{
    public class QuestionApiModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string SampleAnswer { set; get; }

        public long QuestionCategoryId { get; set; }


        
        public List<SkillsTagApiModel>? SkillsTags { get; set; }
        
        public List<long> SkillsTagIds { get; set; }

        //public QuestionStatus status { get; set; }

        public bool SaveAsDraft { set; get; }

        public bool IsEditable { set; get; }
        

    }
}
