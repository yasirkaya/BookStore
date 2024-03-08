using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.DBOperations;
using static WebApi.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using static WebApi.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class AuthorController : ControllerBase
{
    private readonly BookStoreDBContext _context;
    private readonly IMapper _mapper;
    public AuthorController(BookStoreDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAuthor()
    {
        GetAuthorsQuery query = new(_context, _mapper);
        return Ok(query.Handle());
    }

    [HttpGet("{id}")]
    public IActionResult GetAuthorDetail(int id)
    {
        GetAuthorDetailQuery query = new(_context, _mapper);
        query.AuthorId = id;
        GetAuthorDetailQueryValidator validator = new();
        validator.ValidateAndThrow(query);
        return Ok(query.Handle());
    }

    [HttpPost]
    public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
    {
        CreateAuthorCommand command = new(_context, _mapper);
        command.Model = newAuthor;

        CreateAuthorCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAuthor(int id)
    {
        DeleteAuthorCommand command = new(_context);
        command.AuthorId = id;

        DeleteAuthorCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel newAuthor)
    {
        UpdateAuthorCommand command = new(_context);
        command.AuthorId = id;
        command.Model = newAuthor;

        UpdateAuthorCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();

    }
}