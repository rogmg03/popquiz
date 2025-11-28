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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DirectorApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DirectorApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DirectorApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
