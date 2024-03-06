using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommand
{
    private readonly BookStoreDBContext _dbContext;
    public int BookId { get; set; }
    public UpdateBookModel Model { get; set; }
    public UpdateBookCommand(BookStoreDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Handle()
    {
        var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);

        if (book is null)
            throw new InvalidOperationException("Kitap Bulunamadı.");

        book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
        book.Title = string.IsNullOrEmpty(Model.Title.Trim()) ? book.Title : Model.Title;
        _dbContext.SaveChanges();
    }

    public class UpdateBookModel
    {
        public string? Title { get; set; }
        public int GenreId { get; set; }
    }
}