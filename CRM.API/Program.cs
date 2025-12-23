using CRM.API.Service;
using CRM.DataAccess;
using CRM.Model.IdentityModels;
using CRM.Utility;
using CRM.Utility.IUtitlity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using System;
using System.Diagnostics;

namespace CRM.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");;
         
            
            

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
           
            
            
            //service for swagger

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.SignIn.RequireConfirmedAccount = true;
            })
                  .AddEntityFrameworkStores<ApplicationDbContext>()
                  .AddDefaultTokenProviders();

            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddOpenApi();
            //service for swagger




            //service for swagger
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CRM API", Version = "v1" });
            });

            builder.Services.AddSingleton<JsonLocalizationService>();
            //service for swagger


            // service for services
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            //service for services



            // server for utitly 
            builder.Services.AddSingleton<IApplicationEmailSender, ApplicationEmailSender>();
            //server for utility




            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CRM API V1"));

                // Open the default web browser with Swagger UI when the application starts
                var url = "https://localhost:7240/swagger";
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapControllerRoute(
              name: "areas",
              pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );


            app.MapControllers();

            app.Run();
        }
    }
}
