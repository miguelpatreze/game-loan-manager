using IdentityServer.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace IdentityServer.MVC.Data
{
    public class SeedData
    {
        public static async Task EnsureSeedData(IServiceCollection services)
        {
            try
            {
                var userMgr = services.BuildServiceProvider().GetRequiredService<UserManager<ApplicationUser>>();
                var adminUser = await userMgr.FindByNameAsync("miguelpatreze");
                
                if (adminUser == null)
                {
                    adminUser = new ApplicationUser
                    {
                        UserName = "miguelpatreze",
                        Email = "patreze_2@hotmail.com",
                        EmailConfirmed = true
                    };
                    await userMgr.CreateAsync(adminUser, "123456");
                    await userMgr.AddToRoleAsync(adminUser, "Admin");
                }

                var regularUser = await userMgr.FindByNameAsync("miguelpadoze");
           
                if (regularUser == null)
                {
                    regularUser = new ApplicationUser
                    {
                        UserName = "miguelpadoze",
                        Email = "padoze_2@hotmail.com",
                        EmailConfirmed = true
                    };
                    await userMgr.CreateAsync(regularUser, "123456");
                }
            }
            catch (Exception)
            {
                //Log.Error("Error by trying to add default user");
            }
        }
    }
}
