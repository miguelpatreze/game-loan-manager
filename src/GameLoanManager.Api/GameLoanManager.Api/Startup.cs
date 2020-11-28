using AutoMapper;
using FluentValidation.AspNetCore;
using GameLoanManager.Api.Settings;
using GameLoanManager.Api.Swagger;
using GameLoanManager.CrossCutting.Notification;
using GameLoanManager.Domain.Commands.Friends.CreateFriendCommand;
using GameLoanManager.MongoDB;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace GameLoanManager.Api
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
            ConfigureAuthentication(services, Configuration);

            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateFriendCommandValidator>());

            services.AddCors(options => options.AddPolicy("CorsPolicy", policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod().Build();
            }));

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .MinimumLevel.Information()
                .CreateLogger();

            services.AddScoped<INotificationContext, NotificationContext>();

            var assemblyPath = GetType().Assembly.Location;
            var assembly = AppDomain.CurrentDomain.Load("GameLoanManager.Domain");

            services.UseMongoDb(Configuration);
            services.AddSwagger(Configuration, assemblyPath);
            services.AddMediatR(assembly);
            services.AddAutoMapper(assembly);
        }

        private static void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            var identityServerSettings = configuration.GetSection(nameof(IdentityServerSettings)).Get<IdentityServerSettings>();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(
                opt =>
                {
                    opt.Authority = identityServerSettings.Url;
                    opt.Audience = identityServerSettings.ApiResource;
                    opt.RequireHttpsMetadata = false;
                });

            services.AddHttpContextAccessor();

            services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.ConfigureSwagger(Configuration);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
