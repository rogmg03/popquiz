using System.IO;
using Microsoft.AspNetCore.Mvc;
using PopQuiz.Core.BusinessLogic;
using PopQuiz.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PopQuiz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreApiController(IGenreBusiness genreBusiness) : ControllerBase
    {
        // GET: api/<GenreApiController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> Get()
        {
            var genre = await genreBusiness.GetGenres(null);
            return Ok(genre);
        }

        // GET api/<GenreApiController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> Get(int id)
        {
            var genre = await genreBusiness.GetGenres(id);
            var genres = genre.FirstOrDefault();
            if (genres == null)
                return NotFound();
            return Ok(genres);
        }

        // POST api/<GenreApiController>
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] Genre genre)
        {
            var result = await genreBusiness.SaveGenreAsync(genre);
            if (result)
                return CreatedAtAction(nameof(Get), new { id = genre.GenreId }, genre);
            return BadRequest();
        }

        // PUT api/<GenreApiController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] Genre genre)
        {
            if (id != genre.GenreId)
                return BadRequest();

            var result = await genreBusiness.SaveGenreAsync(genre);
            if (result)
                return Ok(result);
            return NotFound();
        }

        // DELETE api/<GenreApiController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await genreBusiness.DeleteGenreAsync(id);
            if (result)
                return Ok(result);
            return NotFound();
        }
    }
}
