using System.ComponentModel.DataAnnotations.Schema;

namespace RPSAcademy.Models
{
    [Table("DefaultSubjects")]
    public class DefaultSubjects
    {
        public int id { get; set; }
        public string? SubjectName { get; set; }
        public string? UserId { get; set;}
        public string? Description { get; set; }
    }
}