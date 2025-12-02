using Microsoft.AspNetCore.Mvc;
using PopQuiz.Core.BusinessLogic;
using PopQuiz.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PopQuiz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieApiController(IMovieBusiness movieBusiness) : ControllerBase
    {
        // GET: api/<MovieApiController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> Get()
        {
            var movies = await movieBusiness.GetMoviesAsync(null);
            return Ok(movies);
        }

        // GET api/<MovieApiController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> Get(int id)
        {
            var movie = await movieBusiness.GetMoviesAsync(id);
            var movies = movie.FirstOrDefault();
            if (movies == null)
                return NotFound();
            return Ok(movies);
        }

        // POST api/<MovieApiController>
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] Movie movie)
        {
            var result = await movieBusiness.SaveCategoryAsync(movie);
            if (result)
                return CreatedAtAction(nameof(Get), new { id = movie.MovieId }, movie);
            return BadRequest();
        }

        // PUT api/<MovieApiController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] Movie movie)
        {
            if (id != movie.MovieId)
                return BadRequest();

            var result = await movieBusiness.SaveCategoryAsync(movie);
            if (result)
                return Ok(result);
            return NotFound();
        }

        // DELETE api/<MovieApiController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await movieBusiness.DeleteMovieAsync(id);
            if (result)
                return Ok(result);
            return NotFound();
        }
    }
}
