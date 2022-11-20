using HackatonWeb.Domain;
using HackatonWeb.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HackatonWeb.Controllers
{
    [ApiController]
    [Route("/")]
    public class YaraResultController : ControllerBase
    {
        private readonly ContextFactory _factory;
        private readonly ILogger<YaraResultController> _logger;

        public YaraResultController(ILogger<YaraResultController> logger)
        {
            _factory = new ContextFactory();
            _logger = logger;
        }

        [HttpGet("yara")]
        public async Task<IEnumerable<YaraResult>> Get()
        {
            using var context = _factory.CreateDbContext(new string[] { });
            var result = await context.YaraResults.ToListAsync();
            
            _logger.LogInformation("GET Request for yara");

            return result;
        }

        [HttpGet("full-yara")]
        public async Task<IEnumerable<FullYaraDto>>GetFullYara()
        {
            using var context = _factory.CreateDbContext(new string[] { });
            var yara = await context.YaraResults.ToListAsync();
            var computers = await context.Computers.ToListAsync();
            var result = computers.Join(yara, x => x.Id, y => y.ComputerId, (x, y) => new FullYaraDto
            {
                PathToFile = y.Path,
                MacAddress = x.MacAddress,
                ComputerName = x.Name
            });
            
            _logger.LogInformation("GET Request full yara");

            return result;
        }

        [HttpPost("yara")]
        public async Task Post([FromBody] YaraResult yaraResult)
        {
            using var context = _factory.CreateDbContext(new string[] { });
            await context.YaraResults.AddAsync(yaraResult);
            await context.SaveChangesAsync();
            
            _logger.LogInformation("POST request yara");
        }
    }
}