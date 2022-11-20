using HackatonWeb.Domain;
using HackatonWeb.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HackatonWeb.Controllers
{
    [ApiController]
    [Route("/")]
    public class ComputerController : ControllerBase
    {
        private readonly ContextFactory _factory;
        private readonly ILogger<ComputerController> _logger;

        public ComputerController(ILogger<ComputerController> logger)
        {
            _factory = new ContextFactory();
            _logger = logger;
        }

        [HttpGet("computers")]
        public async Task<IEnumerable<Computer>> Get()
        {
            using var context = _factory.CreateDbContext(new string[] { });
            var result = await context.Computers.ToListAsync();
            
            _logger.LogInformation("GET Request computers");
            
            return result;
        }

        [HttpPost("computer")]
        public async Task<ActionResult<IEnumerable<Computer>>> Post([FromBody] Computer computer)
        {
            using var context = _factory.CreateDbContext(new string[] { });

            if (context.Computers.Any(x => x.MacAddress.Trim() == computer.MacAddress.Trim()))
            {
                return UnprocessableEntity();
            }
            
            await context.Computers.AddAsync(computer);
            await context.SaveChangesAsync();
            
            var result = await context.Computers.ToListAsync();
            
            _logger.LogInformation("POST Request computers");

            return result;
        }
    }
}