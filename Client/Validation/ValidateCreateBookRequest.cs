using FluentValidation;
using Models;
using System;

namespace Client.Validation
{
    public class ValidateCreateBookRequest : AbstractValidator<CreateBookRequest>
    {
        public ValidateCreateBookRequest()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Name cannot be null")
                .NotEmpty()
                .WithMessage("Name cannot be empty")
                .MaximumLength(100)
                .WithMessage("Name cannot be over 100 symbols");

            RuleFor(x => x.Author)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Author cannot be null")
                .NotEmpty()
                .WithMessage("Author cannot be empty")
                .MaximumLength(50)
                .WithMessage("Author cannot be over 50 symbols");

            RuleFor(x => x.Year)
                .Cascade(CascadeMode.Stop)
                .Must(x => x <= DateTime.Now.Year)
                .WithMessage("Year of publishing cannot be more than this year")
                .Must(x => x > 0)
                .WithMessage("Year of publishing cannot be negative");
        }
    }
}
