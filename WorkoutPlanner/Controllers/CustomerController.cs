using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Data;
using WorkoutPlanner.Models;

namespace WorkoutPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            var customers = await context.Customers.ToListAsync();

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(string id)
        {
            var customer = await context.Customers.FirstOrDefaultAsync(c => c.Id == id);

            return customer is null ? NotFound() : Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        {
            context.Customers.Add(customer);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> UpdateCustomer(string id, Customer customerToAdd)
        {
            if (id != customerToAdd.Id)
            {
                return BadRequest(new {message = "Customer Id mismatch"});
            }

            var customer = await context.Customers.FirstOrDefaultAsync(c => c.Id == id);

            if (customer is null)
            {
                return NotFound();
            }

            context.Entry(customer).CurrentValues.SetValues(customerToAdd);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, new { message = "Error updating customer." });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(string id)
        {
            var customer = await context.Customers.FirstOrDefaultAsync(c => c.Id == id);

            if (customer is null)
            {
                return NotFound();
            }

            context.Customers.Remove(customer);
            await context.SaveChangesAsync();

            return customer;
        }
    }
}
