using Microsoft.AspNetCore.Identity;
using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Seeds
{
    public static class UserAndRoleSeeder
    {
        public static async Task SeedUsersAndRoles(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            // Create the roles that you want to make sure are added 
            var memberRoleExists = await roleManager.RoleExistsAsync("member");
            if (!memberRoleExists)
                await roleManager.CreateAsync(new IdentityRole("member"));

            var adminRoleExists = await roleManager.RoleExistsAsync("administrator");
            if (!adminRoleExists)
                await roleManager.CreateAsync(new IdentityRole("administrator"));

            // Create the users if they don't exist yet so that we have a default user in the DB
            var memberFound = await userManager.FindByNameAsync("fabio.destefani+member@launchpadbyvog.com");
            if (memberFound == null)
            {
                var user = new User
                {
                    UserName = "fabio.destefani+member@launchpadbyvog.com",
                    Email = "fabio.destefani+member@launchpadbyvog.com",
                    FirstName = "fabio.destefani",
                    LastName = "Member",
                };
                IdentityResult result = await userManager.CreateAsync(user, "Password1");

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, "member");
            }
            var adminFound = await userManager.FindByNameAsync("fabio.destefani+admin@launchpadbyvog.com");
            if (adminFound == null)
            {
                var user = new User
                {
                    UserName = "fabio.destefani+admin@launchpadbyvog.com",
                    Email = "fabio.destefani+admin@launchpadbyvog.com",
                    FirstName = "fabio.destefani",
                    LastName = "Admin",
                };
                IdentityResult result = await userManager.CreateAsync(user, "Password1");

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, "administrator");
            }
        }
    }
}
