using QuestionBank.Common.Enumeration;

namespace QuestionBank.Persistence.Entity;

public class UserAccount
{
    public long Id { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string CellNo { get; set; }

    public DateTime RegisteredOn { get; set; }

    public DateTime? PasswordExpiredOn { get; set; }
    
    public UserAccountStatus AccountStatus { get; set; }

    public Person Person { get; set; }


    public long PersonId { get; set; }


    public Role Role { get; set; }

    public long RoleId { get; set; }

    public ICollection<QuestionCategory> QuestionCategories { get; set;}

    public ICollection<QuestionCategoryUserAction> QuestionCategoryUserActions { get; }
    
    public ICollection<QuestionFeedBack> QuestionFeedBacks { get; }
    public ICollection<QuestionFeedBackComment> QuestionFeedBackComments { get; }


    public ICollection<Question> Questions { get; set;}

    public ICollection<SkillsTag> SkillsTags { get; set;}
    
    public ICollection<UserTag> UserTags { get; set;}




}