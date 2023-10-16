using System.ComponentModel.DataAnnotations.Schema;

namespace RPSAcademy.Models
{
    [Table("Opponents")]
    public class Opponents
    {
        public int id { get; set; }
        public string? Name { get; set; }
        public string? Info { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}