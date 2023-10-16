using Microsoft.AspNetCore.Identity;
using RPSAcademy.Constants;

namespace RPSAcademy.Data
{
    public class DbSeeder
    {
        public static async Task SeedDefaultData(IServiceProvider service)
        {
            var userMgr = service.GetService<UserManager<IdentityUser>>();
            var roleMgr = service.GetService<RoleManager<IdentityRole>>();
            // adding roles to db
            await roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleMgr.CreateAsync(new IdentityRole(Roles.User.ToString()));

            var admin = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };

            var UserInDb = await userMgr.FindByEmailAsync(admin.Email);
            if (UserInDb == null)
            {
                await userMgr.CreateAsync(admin, "Admin@123");
                await userMgr.AddToRoleAsync(admin,Roles.Admin.ToString());
            }
        }
    }
}
