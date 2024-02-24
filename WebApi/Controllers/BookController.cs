using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]

public class BookController : ControllerBase
{
    private static List<Book> BookList = new List<Book>()
    {
        new Book{
            Id = 1, 
            Title = "Lean Startup", 
            GenreId = 1, //Personal Growth
            PageCount = 200,
            PublishDate = new DateTime(2001,06,12)
        },
        new Book{
            Id = 2, 
            Title = "Herland", 
            GenreId = 2, //Science Fiction
            PageCount = 250,
            PublishDate = new DateTime(2010,05,23)
        },
        new Book{
            Id = 3, 
            Title = "Dune", 
            GenreId = 2, //Science Fiction
            PageCount = 540,
            PublishDate = new DateTime(2001,12,21)
        }
    };
    [HttpGet] // [HttpGet] parametresiz sadece bir defa çalışabilir.
    public List<Book> GetBooks(){
        return BookList;
    }

    [HttpGet("{id}")]
    public Book GetById(int id){
        return BookList.Where(book => book.Id == id).SingleOrDefault();
    }

    [HttpPost]
    public IActionResult AddBook([FromBody] Book newBook){
        var book = BookList.
            SingleOrDefault(x => x.Title == newBook.Title);
        
        if(book is not null)
            return BadRequest();
        
        BookList.Add(newBook);
        return Ok();

    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] Book updatedBook){
        var book = BookList.SingleOrDefault(x => x.Id == id);

        if(book is null)
            return BadRequest();
        
        book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
        book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
        book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
        book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

        return Ok();

    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id){
        var book = BookList.SingleOrDefault(x => x.Id == id);

        if(book is null)
            return BadRequest();
        
        BookList.Remove(book);
        return Ok();
    }

}
