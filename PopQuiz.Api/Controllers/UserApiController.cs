using Microsoft.AspNetCore.Mvc;
using PopQuiz.Core.BusinessLogic;
using PopQuiz.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PopQuiz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController(IUserBusiness userBusiness) : ControllerBase
    {
        // GET: api/<UserApiController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var user = await userBusiness.GetUsers(null);
            return Ok(user);
        }

        // GET api/<UserApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
