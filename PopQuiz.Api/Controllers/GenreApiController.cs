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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GenreApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GenreApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GenreApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
