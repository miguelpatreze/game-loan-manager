using IdentityServer.MVC.Models;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Linq;

namespace IdentityServer.MVC.Data
{
    public static class SeedData
    {
        public static IApplicationBuilder InitializeDatabase(this IApplicationBuilder app)
        {
            try
            {

                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                    var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                    context.Database.Migrate();

                    if (!context.Clients.Any())
                    {
                        foreach (var client in Config.Clients)
                        {
                            context.Clients.Add(client.ToEntity());
                        }
                        context.SaveChanges();
                    }

                    if (!context.IdentityResources.Any())
                    {
                        foreach (var resource in Config.IdentityResources)
                        {
                            context.IdentityResources.Add(resource.ToEntity());
                        }
                        context.SaveChanges();
                    }

                    if (!context.ApiScopes.Any())
                    {
                        foreach (var resource in Config.ApiScopes)
                        {
                            context.ApiScopes.Add(resource.ToEntity());
                        }
                        context.SaveChanges();
                    }

                    var contextUsers = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                    contextUsers.Database.Migrate();

                    var userMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleMgr = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    var adminRole = roleMgr.FindByNameAsync("ADMIN").Result;
                    if (adminRole is null)
                        roleMgr.CreateAsync(new IdentityRole { Name = "ADMIN" }).Wait();

                    var adminUser = userMgr.FindByNameAsync("miguelpatreze").Result;
                    if (adminUser is null)
                    {
                        adminUser = new ApplicationUser
                        {
                            UserName = "miguelpatreze",
                            Email = "patreze_2@hotmail.com",
                            EmailConfirmed = true
                        };
                        userMgr.CreateAsync(adminUser, "123456").Wait();
                        userMgr.AddToRoleAsync(adminUser, "Admin").Wait();
                    }

                    var regularUser = userMgr.FindByNameAsync("miguelpadoze").Result;
                    if (regularUser is null)
                    {
                        regularUser = new ApplicationUser
                        {
                            UserName = "miguelpadoze",
                            Email = "padoze_2@hotmail.com",
                            EmailConfirmed = true
                        };
                        userMgr.CreateAsync(regularUser, "123456").Wait();
                    }
                }

                return app;
            }
            catch (System.Exception ex)
            {
                Log.Error(ex, "Ocorreu um erro ao inserir os dados inicias do banco de dados.");
                throw ex;
            }

        }
    }
}
