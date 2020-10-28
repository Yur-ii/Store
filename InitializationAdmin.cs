using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Models;
using Store.Controllers;
using Store.Aditional_methods;

namespace Store
{
    public class InitializationAdmin
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@admin.com";
            string adminLogin = "Admin";
            string password = "As12345";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("manager") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("manager"));
            }
            if (await userManager.FindByNameAsync(adminLogin) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminLogin , Image = DefaultImg.UseDefaulImg() };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
