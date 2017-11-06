using GAM.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GAM.Models
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (context.Users.Any(u => u.UserName == "admin"))
            {
                return;
            }

            // Admin Role
            await _roleManager.CreateAsync(new IdentityRole("Admin"));

            // Admin User                
            var user = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@gam.com"
            };

            string userPWD = "Admin123!";

            IdentityResult createUser = await _userManager.CreateAsync(user, userPWD);

            // Admin User - Role  
            if (createUser.Succeeded)
            {
                var result = await _userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
