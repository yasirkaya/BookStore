using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(command => command.Model.Name).MinimumLength(3).NotEmpty();
        RuleFor(command => command.Model.Surname).MinimumLength(2).NotEmpty();
        RuleFor(command => command.Model.Birthday).NotEmpty().LessThan(DateTime.Now.Date);
    }
}