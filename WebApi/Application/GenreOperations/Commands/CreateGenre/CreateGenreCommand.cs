using System.Text;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommand
{
    public CreateGenreModel Model { get; set; }
    private readonly BookStoreDBContext _context;
    private readonly IMapper _mapper;
    public CreateGenreCommand(BookStoreDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        var genre = _context.Genres.SingleOrDefault(x => x.Name == Model.Name);

        if (genre is not null)
            throw new InvalidOperationException("Kitap türü zaten mevcut.");

        genre = _mapper.Map<Genre>(Model);

        _context.Genres.Add(genre);
        _context.SaveChanges();


    }

    public class CreateGenreModel
    {
        public string? Name { get; set; }
    }
}