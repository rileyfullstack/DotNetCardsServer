using DotNetCardsServer.Models.MockData;
using DotNetCardsServer.Models.Users;
using DotNetCardsServer.Services.Users;
using DotNetCardsServer.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DotNetCardsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: UserController
        // GET: api/<UsersController>
        private UsersService _usersService;

        public UserController(IMongoClient mongoClient)

        {
            _usersService = new UsersService(mongoClient);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(MockUsers.UserList);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            User? u = MockUsers.UserList.FirstOrDefault(user => user.Id.ToString() == id);
            if (u == null)
            {
                return NotFound();
            }
            return Ok(u);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User newUser)
        {
            await _usersService.CreateUserAsync(newUser);

            return CreatedAtAction(nameof(Get), new { Id = newUser.Id }, newUser);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] User updatedUser)
        {
            int index = MockUsers.UserList.FindIndex(user => user.Id.ToString() == id);
            if (index == -1)
            {
                return NotFound();
            }

            MockUsers.UserList[index] = ObjectHelper.DeepCopy(updatedUser);

            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            User? u = MockUsers.UserList.FirstOrDefault(user => user.Id.ToString() == id);
            if (u == null)
            {
                return NotFound();
            }

            MockUsers.UserList.Remove(u);

            return NoContent();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            User? u = MockUsers.UserList.FirstOrDefault(user => user.Email == loginModel.Email && user.Password == loginModel.Password);
            if (u == null)
            {
                return Unauthorized("Email or Password wrong");
            }
            return Ok("login token");
        }

    }
}
