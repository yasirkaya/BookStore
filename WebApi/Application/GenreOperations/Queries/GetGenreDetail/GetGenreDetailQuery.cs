using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQuery
{
    public int GenreId { get; set; }
    private readonly BookStoreDBContext _context;
    private readonly IMapper _mapper;
    public GetGenreDetailQuery(BookStoreDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public GenreDetailViewModel Handle()
    {
        var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId && x.IsActive);
        if (genre is null)
            throw new InvalidOperationException("Kitap Türü Bulunamadı.");

        return _mapper.Map<GenreDetailViewModel>(genre);
    }

    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}