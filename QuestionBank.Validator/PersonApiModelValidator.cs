using FluentValidation;
using QuestionBank.Model.Api;

namespace QuestionBank.Validator
{
    public class PersonApiModelValidator : AbstractValidator<PersonApiModel>
    {
        public PersonApiModelValidator() {


            RuleFor(p => p.FirstName).NotEmpty();
            
            
            RuleFor(p => p.LastName).NotEmpty();
           
            RuleFor(p => p.DOB).NotEmpty();
           
            RuleFor(p => p.Address).NotEmpty();
            
            
            



        
        }
    }
}
