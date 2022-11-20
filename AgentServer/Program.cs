using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using HackatonWeb.Domain;
using HackatonWeb.Domain.Entities;

namespace AgentServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var listner = new TcpListener(IPAddress.Parse("192.168.0.103"), 8888);
            var factory = new ContextFactory();

            try
            {
                listner.Start();

                while (true)
                {
                    using var client = await listner.AcceptTcpClientAsync();
                    var stream = client.GetStream();
                    var responseData = new byte[1024];
                    int bytes = 0;
                    var response = new StringBuilder();

                    do
                    {
                        bytes = await stream.ReadAsync(responseData);
                        response.Append(Encoding.UTF8.GetString(responseData, 0, bytes));
                    } while (bytes > 0);
                    
                    if (String.IsNullOrEmpty(response.ToString())) continue;
                    
                    await SaveToDB(response.ToString(), factory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static async Task SaveToDB(string content, ContextFactory factory)
        {
            try
            {
                var entities = JsonSerializer.Deserialize<List<YaraDto>>(
                    content, 
                    new JsonSerializerOptions {PropertyNameCaseInsensitive = false});
                
                if (entities == null) return;
                
                foreach (var entity in entities)
                {
                    using var context = factory.CreateDbContext(new string[] { });
                    var computer = context.Computers.FirstOrDefault(x => x.MacAddress == entity.MacAddress);

                    if (computer == null)
                    {
                        var savedComputer = await context.Computers.AddAsync(new Computer
                        {
                            MacAddress = entity.MacAddress
                        });

                        await context.YaraResults.AddAsync(new YaraResult
                        {
                            ComputerId = savedComputer.Entity.Id,
                            Path = entity.PathToFile
                        });

                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        await context.YaraResults.AddAsync(new YaraResult
                        {
                            ComputerId = computer.Id,
                            Path = entity.PathToFile
                        });

                        await context.SaveChangesAsync();
                    }
                    
                    Console.WriteLine("Add new entity");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}