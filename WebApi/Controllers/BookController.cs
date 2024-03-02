using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookById;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.GetBookById.GetBookByIdQuery;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]

public class BookController : ControllerBase
{
    private readonly BookStoreDBContext _context;

    public BookController(BookStoreDBContext context)
    {
        _context = context;
    }

    [HttpGet] // [HttpGet] parametresiz sadece bir defa çalışabilir.
    public IActionResult GetBooks()
    {
        GetBooksQuery query = new GetBooksQuery(_context);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        BookDetailViewModel result;
        GetBookByIdQuery query = new(_context);
        try
        {
            query.Id = id;
            result = query.Handle();
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }

        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookModel newBook)
    {
        CreateBookCommand command = new(_context);
        try
        {

            command.Model = newBook;
            command.Handle();


        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }

        return Ok();

    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
    {
        try
        {
            UpdateBookCommand command = new(_context);
            command.BookId = id;
            command.Model = updatedBook;
            command.Handle();
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }

        return Ok();

    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        DeleteBookCommand command = new(_context);
        try
        {
            command.BookId = id;
            command.Handle();
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
        return Ok();
    }

}
