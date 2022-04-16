using FluentValidation;
using Models;

namespace Client.Validation
{
    public class ValidateCreateStudentRequest : AbstractValidator<CreateStudentRequest>
    {
        public ValidateCreateStudentRequest()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Name cannot be null")
                .NotEmpty()
                .WithMessage("Name cannot be empty")
                .MaximumLength(50)
                .WithMessage("Name cannot be over 50 symbols");

            RuleFor(x => x.Age)
                .Cascade(CascadeMode.Stop)
                .Must(x => x > 3)
                .WithMessage("Student's age cannot be less than 3")
                .Must(x => x < 50)
                .WithMessage("Student's age cannot be more than 50");
        }
    }
}
