using DotNetCardsServer.Exceptions;
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
        public async Task<IActionResult> Get()
        {
            List<User> result = await _usersService.GetUsersAsync();
            return Ok(result);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                User u = await _usersService.GetUserAsync(id);
                return Ok(u);
            }
            catch (UserDoesntExistException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User newUser)
        {
            try 
            {
                await _usersService.CreateUserAsync(newUser);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(Get), new { Id = newUser.Id }, newUser);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] User updatedUser)
        {
            try
            {
                User newUser = await _usersService.EditUserAsync(id, updatedUser);
            }
            catch (UserDoesntExistException ex)
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }


        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _usersService.DeleteUserAsync(id);
            }
            catch (UserDoesntExistException ex)
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                User? u = await _usersService.LoginAsync(loginModel);
            } catch(AuthenticationException ex)
            {
                Console.WriteLine(ex.Message);
                return Unauthorized("Email or Password wrong");

            }
            return Ok("login token");
        }

    }
}
