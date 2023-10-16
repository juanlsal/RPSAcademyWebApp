using Microsoft.AspNetCore.Identity;

namespace RPSAcademy.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? PreferredName { get; set; }

        public string? ProfileURL { get; set; }

    }
}
