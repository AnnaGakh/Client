using Models;
using FluentValidation;

namespace Client.Validation
{
    public class ValidateCreateSchoolRequest : AbstractValidator<CreateSchoolRequest>
    {
        public ValidateCreateSchoolRequest()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Name cannot be null")
                .NotEmpty()
                .WithMessage("Name cannot be empty")
                .MaximumLength(30)
                .WithMessage("Name cannot be over 30 symbols");

            RuleFor(x => x.Number)
                .Cascade(CascadeMode.Stop)
                .Must(x => x > 0)
                .WithMessage("School number cannot be negative");

            RuleFor(x => x.Headmaster)
               .Cascade(CascadeMode.Stop)
               .NotNull()
               .WithMessage("Name cannot be null")
               .MaximumLength(50)
               .WithMessage("Name cannot be over 50 symbols");
        }
    }
}
