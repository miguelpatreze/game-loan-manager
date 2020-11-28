using IdentityServer.MVC.Data;
using IdentityServer.MVC.Data.Stores;
using IdentityServer.MVC.Models;
using IdentityServer.MVC.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using MongoDB.Driver;
using StackExchange.Redis;

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

            var redisCacheSettings = Configuration.GetSection(nameof(RedisSettings)).Get<RedisSettings>();

            services.AddDataProtection(opts =>
            {
                opts.ApplicationDiscriminator = "identity.server.mvc";
            })
            .PersistKeysToStackExchangeRedis(ConnectionMultiplexer.Connect(redisCacheSettings.ConnectionString), "DataProtection-Keys");

            var mongoSettings = Configuration.GetSection(nameof(MongoSettings)).Get<MongoSettings>();
            services.AddSingleton(mongoSettings);
            services.AddSingleton<IMongoClient>(new MongoClient(mongoSettings.ConnectionString));

            services.AddIdentity<ApplicationUser, IdentityRole<ObjectId>>(
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
                .AddUserStore<UserStore>()
                .AddRoleStore<RoleStore>()
                .AddDefaultTokenProviders();

            services.AddSingleton<IUserStore<ApplicationUser>, UserStore>();
            services.AddSingleton<IRoleStore<IdentityRole<ObjectId>>, RoleStore>();

            services.AddIdentityServer(opt =>
            {
                opt.IssuerUri = Configuration.GetValue<string>("Issuer_Uri");
                opt.UserInteraction.LoginUrl = "/Account/Login";
                opt.UserInteraction.LogoutUrl = "/Account/Logout";
            })
                .AddAspNetIdentity<ApplicationUser>()
                .AddInMemoryApiResources(Config.ApiResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryPersistedGrants()
                .AddDeveloperSigningCredential();

            SeedData.EnsureSeedData(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
    }
}
