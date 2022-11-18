using HackatonWeb.Domain;
using HackatonWeb.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HackatonWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class YaraResultController : ControllerBase
    {
        private readonly ContextFactory _factory;

        public YaraResultController() => _factory = new ContextFactory();

        [HttpGet("yara-results")]
        public async Task<IEnumerable<YaraResult>> Get()
        {
            using var context = _factory.CreateDbContext(new string[] { });
            return await context.YaraResults.ToListAsync();
        }

        [HttpPost("yara-results")]
        public async Task Post([FromBody] YaraResult yaraResult)
        {
            using var context = _factory.CreateDbContext(new string[] { });
            await context.YaraResults.AddAsync(yaraResult);
            await context.SaveChangesAsync();
        }
    }
}