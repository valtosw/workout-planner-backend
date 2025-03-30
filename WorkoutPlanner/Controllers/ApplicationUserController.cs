using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Data;
using WorkoutPlanner.Models;

namespace WorkoutPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController(AppDbContext context) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ApplicationUser> GetUserById(string id)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
            return user!;
        }

        [HttpGet("Role/{id}")]
        public async Task<string> GetUserRoleById(string id)
        {
            var userRoleRow = await context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == id);

            var userRole = await context.Roles
                .FirstOrDefaultAsync(r => r.Id == userRoleRow!.RoleId);

            return userRole!.Name!;
        }
    }
}
