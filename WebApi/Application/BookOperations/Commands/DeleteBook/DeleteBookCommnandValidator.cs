using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.DeleteBook;

public class DeleteBookCommnandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommnandValidator()
    {
        RuleFor(command => command.BookId).GreaterThan(0);
    }
}