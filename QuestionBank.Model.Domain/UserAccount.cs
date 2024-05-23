using QuestionBank.Common.Enumeration;

namespace QuestionBank.Model.Domain;

public class UserAccount
{
    public int Id { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string CellNo { get; set; }

    public DateTime RegisteredOn { get; set; }

    public DateTime? PasswordExpiredOn { get; set; }

    public UserAccountStatus AccountStatus { get; set; }

    public long RoleId { get; set; }

    public Person Person { get; set; }

    public List<SkillsTag> SkillsTags { get; set; }
    public List<long> TagIds { get; set; }

    




}