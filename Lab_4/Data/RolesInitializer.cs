using Lab_4.Models;
using Microsoft.AspNetCore.Identity;

namespace Lab_4.Data
{
    public class RolesInitializer
    {
        public static async Task<int> Initialize(HttpContext context)
        {

            UserManager<User> userManager = context.RequestServices.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = context.RequestServices.GetRequiredService<RoleManager<IdentityRole>>();

            string adminEmail = "main_admin@gmail.com";
            string password = "Zxcvbnm_123";

       
            if (await roleManager.FindByNameAsync("MainAdmin") == null)
            {

                await roleManager.CreateAsync(new IdentityRole("MainAdmin"));

                User admin = new User { Email = adminEmail, UserName = adminEmail };

                IdentityResult result = await userManager.CreateAsync(admin, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "MainAdmin");
                }

            }
            
            if (await roleManager.FindByNameAsync("AdmissionOfficer") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("AdmissionOfficer"));
            }
            if (await roleManager.FindByNameAsync("JuniorAdmin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("JuniorAdmin"));
            }

            return 0;
        }
    }
}
