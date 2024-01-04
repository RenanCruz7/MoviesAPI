using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MoviesAPI.Data;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{

    private MovieContext _context;

    public MovieController(MovieContext context)
    {
        _context = context;
    }



    [HttpGet]
    // Skip e take paginando as consultas
    public IEnumerable<Movie> ListMovies([FromQuery]int skip = 0, int take = 50)
    {
        return _context.Movies.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult ListMovieByID(int id)
    {
        var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
        if (movie == null) return NotFound();
        return Ok(movie);
    }

    [HttpPost]
    public CreatedAtActionResult AddMovie ([FromBody]Movie movie)
    {
        _context.Movies.Add(movie);
        _context.SaveChanges();
        return CreatedAtAction(nameof(ListMovieByID), new {id = movie.Id},movie);
    }
}
