using Microsoft.AspNetCore.Http.HttpResults;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookById;

public class GetBookByIdQuery
{
    public int Id { get; set; }
    private readonly BookStoreDBContext _dbContext;
    public GetBookByIdQuery(BookStoreDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public BookDetailViewModel Handle()
    {
        var book = _dbContext.Books.Where(book => book.Id == Id).SingleOrDefault();
        if (book is null)
            throw new InvalidOperationException("Kitap Bulunamadı.");
        BookDetailViewModel model = new()
        {
            Title = book.Title,
            Genre = ((GenreEnum)book.GenreId).ToString(),
            PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
            PageCount = book.PageCount
        };
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