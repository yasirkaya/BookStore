using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DBOperations;
using static WebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;
using static WebApi.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]

public class GenreController : ControllerBase
{
    private readonly BookStoreDBContext _context;
    private readonly IMapper _mapper;
    public GenreController(BookStoreDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetGenres()
    {
        GetGenresQuery query = new(_context, _mapper);
        return Ok(query.Handle());
    }

    [HttpGet("{id}")]
    public IActionResult GetGenreDetail(int id)
    {
        GetGenreDetailQuery query = new(_context, _mapper);
        query.GenreId = id;
        GetGenreDetailQueryValidator validator = new();
        validator.ValidateAndThrow(query);

        return Ok(query.Handle());
    }

    [HttpPost]
    public IActionResult AddGenre([FromBody] CreateGenreModel genre)
    {
        CreateGenreCommand command = new(_context, _mapper);
        command.Model = genre;

        CreateGenreCommandValidator validator = new();
        validator.ValidateAndThrow(command);
        command.Handle();

        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel genre)
    {
        UpdateGenreCommand command = new(_context);
        command.Model = genre;
        command.GenreId = id;

        UpdateGenreCommandValidaor validator = new();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteGenre(int id)
    {
        DeleteGenreCommand command = new(_context);
        command.GenreId = id;

        DeleteGenreCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }
}