using HackatonWeb.Domain;
using HackatonWeb.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HackatonWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComputerController : ControllerBase
    {
        private readonly ContextFactory _factory;

        public ComputerController() => _factory = new ContextFactory();

        [HttpGet("computers")]
        public async Task<IEnumerable<Computer>> Get()
        {
            using var context = _factory.CreateDbContext(new string[] { });
            return await context.Computers.ToListAsync();
        }

        [HttpPost("computers")]
        public async Task Post([FromBody] Computer computer)
        {
            using var context = _factory.CreateDbContext(new string[] { });
            await context.Computers.AddAsync(computer);
            await context.SaveChangesAsync();
        }
    }
}