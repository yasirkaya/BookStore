using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(command => command.AuthorId).GreaterThan(0);
        RuleFor(command => command.Model.Name).MinimumLength(3);
        RuleFor(command => command.Model.Surname).MinimumLength(2);
        RuleFor(command => command.Model.Birthday).NotEmpty().LessThan(DateTime.Now.Date);
    }
}