using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommand
{
    public int AuthorId { get; set; }
    private readonly BookStoreDBContext _context;
    public DeleteAuthorCommand(BookStoreDBContext context)
    {
        _context = context;
    }
    public void Handle()
    {
        var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
        if (author is null)
            throw new InvalidOperationException("Yazar Bulunamadı.");

        var hasPublishBook = _context.Books.Include(x => x.Author).Any(x => x.AuthorId == AuthorId);
        if (hasPublishBook)
            throw new InvalidOperationException("Yazarın yayında kitabı mevcut. Silinemez!");

        _context.Authors.Remove(author);
        _context.SaveChanges();
    }
}