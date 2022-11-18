namespace HackatonWeb.Domain.Entities
{
    public class Computer : BaseEntity
    {
        public string MacAddress { get; set; }
        
        public string Name { get; set; }
    }
}