using System.Data.Common;
using AutoMapper;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks;

class GetBooksQuery
{
    private readonly BookStoreDBContext _dbContext;
    private readonly IMapper _mapper;

    public GetBooksQuery(BookStoreDBContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<BookViewModel> Handle()
    {
        var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();
        List<BookViewModel> vm = _mapper.Map<List<BookViewModel>>(bookList);
        return vm;
    }

    public class BookViewModel
    {
        public string? Title { get; set; }
        public int PageCount { get; set; }
        public string? PublishDate { get; set; }
        public string? Genre { get; set; }
    }
}