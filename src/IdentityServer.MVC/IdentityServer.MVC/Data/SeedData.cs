using IdentityServer.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IdentityServer.MVC.Data
{
    public class SeedData
    {
        public static void EnsureSeedData(IServiceCollection services)
        {
            try
            {
                var userMgr = services.BuildServiceProvider().GetRequiredService<UserManager<ApplicationUser>>();
                var defaultUser = userMgr.FindByNameAsync("miguelpatreze").Result;
                if (defaultUser == null)
                {
                    defaultUser = new ApplicationUser
                    {
                        UserName = "miguelpatreze",
                        Email = "patreze_2@hotmail.com",
                        EmailConfirmed = true
                    };
                    var result = userMgr.CreateAsync(defaultUser, "123456").Result;
                }
            }
            catch (Exception)
            {
                //Log.Error("Error by trying to add default user");
            }
        }
    }
}
