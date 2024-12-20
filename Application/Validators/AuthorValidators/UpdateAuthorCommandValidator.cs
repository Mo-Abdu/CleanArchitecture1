
using Application.Commands.AuthorCommands;
using FluentValidation;

namespace Application.Validators.AuthorValidators
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.");

            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Author name is required.")
                .MaximumLength(100).WithMessage("Author name cannot exceed 100 characters.");
        }
    }
}
