using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors;

public class GetAuthorsQuery
{
    private readonly BookStoreDBContext _context;
    private readonly IMapper _mapper;

    public GetAuthorsQuery(BookStoreDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<AuthorViewModel> Handle()
    {
        var author = _context.Authors.OrderBy(x => x.Id).ToList();
        return _mapper.Map<List<AuthorViewModel>>(author);

    }

    public class AuthorViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime Birthday { get; set; }
    }
}