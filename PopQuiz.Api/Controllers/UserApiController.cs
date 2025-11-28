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

        // GET api/UserApiController/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var users = await userBusiness.GetUsers(id);
            var user = users.FirstOrDefault();
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // POST api/UserApiController
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] User user)
        {
            var result = await userBusiness.SaveUserAsync(user);
            if (result)
                return CreatedAtAction(nameof(Get), new { id = user.UserId }, user);
            return BadRequest();
        }

        // PUT api/UserApiController/5
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] User user)
        {
            if (id != user.UserId)
                return BadRequest();

            var result = await userBusiness.SaveUserAsync(user);
            if (result)
                return Ok(result);
            return NotFound();
        }

        // DELETE api/UserApiController/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await userBusiness.DeleteUserAsync(id);
            if (result)
                return Ok(result);
            return NotFound();
        }
    }
}
