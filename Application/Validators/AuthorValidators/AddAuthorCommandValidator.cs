using Application.Commands.AuthorCommands;
using FluentValidation;

namespace Application.Validators.AuthorValidators
{
    public class AddAuthorCommandValidator : AbstractValidator<AddAuthorCommand>
    {
        public AddAuthorCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Author name is required.")
                .MaximumLength(100).WithMessage("Author name cannot exceed 100 characters.");
        }
    }
}
