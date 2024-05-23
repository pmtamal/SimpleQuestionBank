namespace QuestionBank.Model.Domain;

public class Person
{
    public long Id { get; set; }

    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public DateTime DOB { get; set; }

    public string Photo { get; set; }


    public ICollection<UserAccount> UserAccounts { get; set; }


    public string FullName => $"{FirstName} {MiddleName} {LastName}";
}