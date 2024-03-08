using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BookStoreDBContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDBContext>>()))
        {
            if (context.Books.Any())
            {
                return;
            }

            context.Authors.AddRange(
                new Author
                {
                    Name = "Yasir",
                    Surname = "KAYA",
                    Birthday = new DateTime(1980, 01, 10)
                },
                new Author
                {
                    Name = "Ali",
                    Surname = "USTA",
                    Birthday = new DateTime(1980, 01, 10)
                },
                new Author
                {
                    Name = "Veli",
                    Surname = "AYDIN",
                    Birthday = new DateTime(1980, 01, 10)
                }

            );

            context.Genres.AddRange(
                new Genre
                {
                    Name = "Personal Growth"
                },
                new Genre
                {
                    Name = "Science Fiction"
                },
                new Genre
                {
                    Name = "Romance"
                }
            );

            context.Books.AddRange(
                new Book
                {
                    // Id = 1,   [DatabaseGenerated(DatabaseGeneratedOption.Identity)] ile otomatikleştirdik.
                    Title = "Lean Startup",
                    GenreId = 1, //Personal Growth
                    PageCount = 200,
                    PublishDate = new DateTime(2001, 06, 12),
                    AuthorId = 1
                },
                new Book
                {
                    // Id = 2, 
                    Title = "Herland",
                    GenreId = 2, //Science Fiction
                    PageCount = 250,
                    PublishDate = new DateTime(2010, 05, 23),
                    AuthorId = 2
                },
                new Book
                {
                    // Id = 3, 
                    Title = "Dune",
                    GenreId = 2, //Science Fiction
                    PageCount = 540,
                    PublishDate = new DateTime(2001, 12, 21),
                    AuthorId = 1
                }
            );

            context.SaveChanges();
        }
    }
}