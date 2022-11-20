namespace HackatonWeb.Domain.Entities
{
    public class FullYaraDto
    {
        public string PathToFile { get; set; }

        public string MacAddress { get; set; }
        
        public string? ComputerName { get; set; }
    }
}