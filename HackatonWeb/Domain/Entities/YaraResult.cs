using System.ComponentModel.DataAnnotations.Schema;

namespace HackatonWeb.Domain.Entities
{
    public class YaraResult : BaseEntity
    {
        [ForeignKey("Computer")]
        public int ComputerId { get; set; }
        
        public string Path { get; set; }
    }
}