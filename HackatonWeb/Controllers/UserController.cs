using HackatonWeb.Domain;
using HackatonWeb.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HackatonWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ContextFactory _factory;

        public UserController() => _factory = new ContextFactory();
        
        [HttpGet("users")]
        public async Task<IEnumerable<User>> Get()
        {
            using var context = _factory.CreateDbContext(new string[] {});
            return await context.Users.ToListAsync();
        }

        [HttpPost("users")]
        public async Task Post([FromBody] User user)
        {
            using var context = _factory.CreateDbContext(new string[] {});
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }
    }
}