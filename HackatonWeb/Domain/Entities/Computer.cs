using System.ComponentModel.DataAnnotations;

namespace HackatonWeb.Domain.Entities
{
    public class Computer : BaseEntity
    {
        [Required]
        public string MacAddress { get; set; }
        
        public string? Name { get; set; }
    }
}