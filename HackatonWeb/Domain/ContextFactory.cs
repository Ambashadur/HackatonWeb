using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HackatonWeb.Domain
{
    public class ContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            var builder = new ConfigurationBuilder();
            builder.SetBasePath($"{Environment.CurrentDirectory}/");
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();

            var connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}