using IdentityServer.MVC.Data;
using IdentityServer.MVC.Models;
using IdentityServer.MVC.Settings;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using StackExchange.Redis;
using System.Linq;
using System.Reflection;

namespace IdentityServer.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            const string connectionString = @"Server=localhost;Database=IdentityServer;User Id=sa;Password=123456;";

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .MinimumLevel.Information()
                .CreateLogger();

            var redisCacheSettings = Configuration.GetSection(nameof(RedisSettings)).Get<RedisSettings>();

            services.AddDataProtection(opts =>
            {
                opts.ApplicationDiscriminator = "identity.server.mvc";
            })
            .PersistKeysToStackExchangeRedis(ConnectionMultiplexer.Connect(redisCacheSettings.ConnectionString), "DataProtection-Keys");

            services.AddIdentity<ApplicationUser, IdentityRole>(
                    options =>
                    {
                        options.Password.RequireLowercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequiredLength = 6;
                        options.Password.RequireDigit = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequiredUniqueChars = 0;
                        options.User.RequireUniqueEmail = false;
                    })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer(opt =>
            {
                opt.IssuerUri = Configuration.GetValue<string>("Issuer_Uri");
                opt.UserInteraction.LoginUrl = "/Account/Login";
                opt.UserInteraction.LogoutUrl = "/Account/Logout";
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddAspNetIdentity<ApplicationUser>()
            .AddDeveloperSigningCredential();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            InitializeDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=login}");
            });
        }

        private void InitializeDatabase(IApplicationBuilder app)
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
        }
    }
}
