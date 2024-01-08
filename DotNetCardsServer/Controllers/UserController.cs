using DotNetCardsServer.Models.MockData;
using DotNetCardsServer.Models.Users;
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

        



        [HttpGet]
        public IActionResult Get(ObjectId? id)
        {
            if (id != null)
            {
                return Ok(MockUsers.UserList.FirstOrDefault(u => u.Id == id));
            }
            else return Ok(MockUsers.UserList);
        }
        [HttpPut("{id}")]
        public IActionResult Put(ObjectId id, [FromBody] User updatedUser)
        {
            int index = MockUsers.UserList.FindIndex(user => user.Id == id);
            if (index == -1)
            {
                return NotFound();
            }

            MockUsers.UserList[index] = ObjectHelper.DeepCopy(updatedUser);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(ObjectId id)
        {
            User? u = MockUsers.UserList.FirstOrDefault(user => user.Id == id);
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
