using HackatonWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HackatonWeb.Domain
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Computer> Computers { get; set; } = null!;
        public DbSet<YaraResult> YaraResults { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql();
        }
    }
}