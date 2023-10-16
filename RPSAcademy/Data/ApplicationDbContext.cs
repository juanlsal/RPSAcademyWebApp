using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RPSAcademy.Models;

namespace RPSAcademy.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<UserInfo> UserInfo { get; set; }

        public DbSet<Opponents> Opponents { get; set;}

        public DbSet<DefaultSubjects> DefaultSubjects { get; set; }

        public DbSet<DefaultQuestions> DefaultQuestions { get; set; }

        public DbSet<UserCreatedSubjects> UserCreatedSubjects { get; set; }

        public DbSet<UserCreatedQuestions> UserCreatedQuestions { get; set; }


    }
}