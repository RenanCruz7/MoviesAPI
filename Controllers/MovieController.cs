using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MoviesAPI.Data;
using MoviesAPI.Data.Dtos;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{

    private MovieContext _context;
    private IMapper _mapper;
    public MovieController(MovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    // Skip e take paginando as consultas
    public IEnumerable<ReadMovieDto> ListMovies([FromQuery]int skip = 0, int take = 50)
    {
        return _mapper.Map<List<ReadMovieDto>>(_context.Movies.Skip(skip).Take(take));
    }

    [HttpGet("{id}")]
    public IActionResult ListMovieByID(int id)
    {
        var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
        if (movie == null) return NotFound();
        var movieDto = _mapper.Map<ReadMovieDto>(movie);
        return Ok(movieDto);
    }

    [HttpPost]
    public CreatedAtActionResult AddMovie ([FromBody]CreateMovieDto movieDto)
    {
        Movie movie = _mapper.Map<Movie>(movieDto);
        _context.Movies.Add(movie);
        _context.SaveChanges();
        return CreatedAtAction(nameof(ListMovieByID), new {id = movie.Id},movie);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieDto movieDto)
    {
        var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
        if (movie == null) return NotFound();
        _mapper.Map(movieDto,movie);
        _context.SaveChanges();
        return NoContent();
    }


    [HttpPatch("{id}")]
    public IActionResult PatchMovie(int id,JsonPatchDocument<UpdateMovieDto> patch)
    {
        var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
        if (movie == null) return NotFound();

        var movieToPatch = _mapper.Map<UpdateMovieDto>(movie);

        patch.ApplyTo(movieToPatch, ModelState);

        if (!TryValidateModel(movieToPatch))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(movieToPatch, movie);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMovie(int id)
    {
        var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
        if (movie == null) return NotFound();
        _context.Remove(movie);
        _context.SaveChanges();
        return NoContent();
    }
}
