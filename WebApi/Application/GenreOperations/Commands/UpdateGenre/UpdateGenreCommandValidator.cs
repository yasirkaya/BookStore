using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommandValidaor : AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidaor()
    {
        RuleFor(command => command.Model.Name).MinimumLength(4).When(x => x.Model.Name.Trim() != string.Empty);
    }
}