using System.ComponentModel.DataAnnotations.Schema;

namespace RPSAcademy.Models
{
    [Table("UserCreatedSubjects")]
    public class UserCreatedSubjects
    {
        public int Id { get; set; }
        public string? SubjectName { get; set; }
        public string? UserId { get; set; }
        public string? Description { get; set; }
    }
}
