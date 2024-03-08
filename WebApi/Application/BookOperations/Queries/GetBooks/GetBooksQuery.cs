using System.Data.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Queries.GetBooks;

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
        var bookList = _dbContext.Books.Include(x => x.Genre).Include(x => x.Author).OrderBy(x => x.Id).ToList<Book>();
        List<BookViewModel> vm = _mapper.Map<List<BookViewModel>>(bookList);
        return vm;
    }

    public class BookViewModel
    {
        public string? Title { get; set; }
        public int PageCount { get; set; }
        public string? PublishDate { get; set; }
        public string? Genre { get; set; }
        public string? Author { get; set; }
    }
}