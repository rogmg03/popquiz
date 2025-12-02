using Microsoft.AspNetCore.Mvc;
using PopQuiz.Core.BusinessLogic;
using PopQuiz.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PopQuiz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorApiController(IDirectorBusiness directorBusiness) : ControllerBase
    {
        // GET: api/<DirectorApiController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Director>>> Get()
        {
            var director = await directorBusiness.GetDirectors(null);
            return Ok(director);
        }

        // GET api/<DirectorApiController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Director>> Get(int id)
        {
            var director = await directorBusiness.GetDirectors(id);
            var directors = director.FirstOrDefault();
            if (directors == null)
                return NotFound();
            return Ok(directors);
        }

        // POST api/<DirectorApiController>
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] Director director)
        {
            var result = await directorBusiness.SaveDirectorAsync(director);
            if (result)
                return CreatedAtAction(nameof(Get), new { id = director.DirectorId }, director);
            return BadRequest();
        }

        // PUT api/<DirectorApiController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] Director director)
        {
            if (id != director.DirectorId)
                return BadRequest();

            var result = await directorBusiness.SaveDirectorAsync(director);
            if (result)
                return Ok(result);
            return NotFound();
        }

        // DELETE api/<DirectorApiController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await directorBusiness.DeleteDirectorAsync(id);
            if (result)
                return Ok(result);
            return NotFound();
        }
    }
}
