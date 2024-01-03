using DotNetCardsServer.Models;
using DotNetCardsServer.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCardsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        static private List<Customer> myCustomers = new List<Customer>()
        {
            new Customer(15, "Tzach", 20),
            new Customer(100, "Avi", 15),
            new Customer(300, "Mor", 10)
        };

        // GET: api/customers
        [HttpGet]
        public IActionResult Get(int? minAge)
        {
            if (minAge.HasValue)
            {
                var filteredCustomers = myCustomers.Where((c) => c.Age > minAge);
                return Ok(filteredCustomers);
            }
            return Ok(myCustomers);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customer = myCustomers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound($"Customer with ID {id} not found.");
            }
            return Ok(customer);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = myCustomers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound($"Customer with ID {id} not found.");
            }
            myCustomers.Remove(customer);
            return NoContent();
        }

        
    }
}
