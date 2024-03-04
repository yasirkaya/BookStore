using FluentValidation;

namespace WebApi.BookOperations.DeleteBook;

public class DeleteBookCommnandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommnandValidator()
    {
        RuleFor(command => command.BookId).GreaterThan(0);
    }
}