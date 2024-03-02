using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookById;

public class GetBookByIdQuery
{
    public int Id { get; set; }
    private readonly BookStoreDBContext _dbContext;
    private readonly IMapper _mapper;
    public GetBookByIdQuery(BookStoreDBContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public BookDetailViewModel Handle()
    {
        var book = _dbContext.Books.Where(book => book.Id == Id).SingleOrDefault();
        if (book is null)
            throw new InvalidOperationException("Kitap Bulunamadı.");
        BookDetailViewModel model = _mapper.Map<BookDetailViewModel>(book);
        return model;
    }

    public class BookDetailViewModel
    {
        public string? Title { get; set; }
        public int PageCount { get; set; }
        public string? PublishDate { get; set; }
        public string? Genre { get; set; }
    }
}