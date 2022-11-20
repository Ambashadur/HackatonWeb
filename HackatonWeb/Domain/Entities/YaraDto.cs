namespace HackatonWeb.Domain.Entities
{
    public class YaraDto
    {
        public string PathToFile { get; set; }
        
        public string Rule { get; set; }
        
        public string RuleKey { get; set; }
        
        public string MacAddress { get; set; }
    }
}