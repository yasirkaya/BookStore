using AutoMapper;
using FluentValidation;
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
    private readonly IMapper _mapper;

    public BookController(BookStoreDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet] // [HttpGet] parametresiz sadece bir defa çalışabilir.
    public IActionResult GetBooks()
    {
        GetBooksQuery query = new GetBooksQuery(_context, _mapper);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        BookDetailViewModel result;
        GetBookByIdQuery query = new(_context, _mapper);
        try
        {
            query.Id = id;
            GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
            validator.ValidateAndThrow(query);
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
        CreateBookCommand command = new(_context, _mapper);
        try
        {

            command.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            //ValidationResult result = validator.Validate(command);
            validator.ValidateAndThrow(command);
            command.Handle();
            // if (!result.IsValid)
            // {
            //     foreach (var item in result.Errors)
            //     {
            //         System.Console.WriteLine("Özellik " + item.PropertyName + "- Error Message: " + item.ErrorMessage);
            //     }
            // }
            // else
            // {
            //     
            // }



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
            UpdateBookCommandValidator validator = new();
            validator.ValidateAndThrow(command);
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
            DeleteBookCommnandValidator validator = new DeleteBookCommnandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
        return Ok();
    }

}
