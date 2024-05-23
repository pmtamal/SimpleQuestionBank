

using System.ComponentModel.DataAnnotations.Schema;

namespace QuestionBank.Persistence.Entity;

public class Person
{
    public long Id { get; set; }

    public required string FirstName { get; set; }
    public string MiddleName { get; set; } = string.Empty;
    public required string LastName { get; set; }
    public required string Address { get; set; }
    public DateTime DOB { get; set; }

    public string? Photo { get; set; }


    public required ICollection<UserAccount> UserAccounts { get; set; }

    [NotMapped]
    public string FullName => $"{FirstName} {MiddleName} {LastName}";

}