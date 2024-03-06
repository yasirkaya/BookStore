using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations;

public class BookStoreDBContext : DbContext
{
    public BookStoreDBContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Book> Books { get; set; }
}