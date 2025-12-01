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
        public string Get(int id)
        {
            return "value";
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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MovieApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
