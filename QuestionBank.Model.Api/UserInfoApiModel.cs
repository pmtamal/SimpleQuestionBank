namespace QuestionBank.Model.Api
{
    public class UserInfoApiModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string CellNo { get; set; }
        public string Role { get; set; }

        public string SkillsTag { get; set; }
    }
}
