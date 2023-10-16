using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RPSAcademy.Models
{
    [Table("UserInfo")]
    public class UserInfo
    {

        public int Id { get; set; }
        public string? PreferredName { get; set; }
        public string? ProfileImageUrl { get; set; }

        public string? UserId { get; set; }

    }
}
