using FluentValidation;
using QuestionBank.Model.Api;

namespace QuestionBank.Validator
{
    public class UserAccountValidator : AbstractValidator<UserAccountApiModel>
    {
        public UserAccountValidator()
        {
            RuleFor(uac => uac.Email).NotEmpty().NotNull().EmailAddress();

            RuleFor(uac => uac.CellNo).MinimumLength(11).MaximumLength(15);

            RuleFor(uac => uac.RoleId).NotEmpty().NotNull().GreaterThan(0);

            RuleFor(x => x.Person).SetValidator(new PersonApiModelValidator());





        }
    }
}
