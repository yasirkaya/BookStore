using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommand
{
    private readonly BookStoreDBContext _dbContext;
    public int AuthorId { get; set; }
    public UpdateAuthorModel Model { get; set; }
    public UpdateAuthorCommand(BookStoreDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
        if (author is null)
            throw new InvalidOperationException("Yazar Bulunamadı.");

        author.Name = string.IsNullOrEmpty(Model.Name) ? author.Name : Model.Name;
        author.Surname = string.IsNullOrEmpty(Model.Surname) ? author.Surname : Model.Surname;
        author.Birthday = Model.Birthday.ToString("MM/dd/yyyy") == DateTime.Now.ToString("MM/dd/yyyy") ?
                            author.Birthday : Model.Birthday;

        _dbContext.SaveChanges();


    }

    public class UpdateAuthorModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime Birthday { get; set; }
    }
}

