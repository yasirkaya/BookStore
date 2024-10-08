using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenres;

public class GetGenresQuery
{
    private readonly BookStoreDBContext _context;
    private readonly IMapper _mapper;
    public GetGenresQuery(BookStoreDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<GenreViewModel> Handle()
    {
        var genres = _context.Genres.Where(x => x.IsActive).OrderBy(x => x.Id);
        List<GenreViewModel> returnObj = _mapper.Map<List<GenreViewModel>>(genres);
        return returnObj;
    }

    public class GenreViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}