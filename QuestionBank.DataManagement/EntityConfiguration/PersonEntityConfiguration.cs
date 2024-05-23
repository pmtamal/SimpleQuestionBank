using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestionBank.Persistence.Entity;

namespace QuestionBank.DataManagement.EntityConfiguration;

public class PersonEntityConfiguration: IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {

        builder.ToTable("Person");

        builder.HasKey(person => person.Id);

        builder.Property(person => person.FirstName).IsRequired().HasMaxLength(255);
        
        builder.Property(person => person.LastName).IsRequired().HasMaxLength(255);

        builder.Property(person => person.DOB).IsRequired();

        builder.Property(person => person.Address).HasMaxLength(4000);

        
    }
}