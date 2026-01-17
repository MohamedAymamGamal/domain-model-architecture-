using CRM.DataAccess;
using CRM.Model.IdentityModels;
using CRM.Utility;
using CRM.Utility.IUtitlity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CRM.Service.Identity;


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

            //service for swagger




            //service for swagger
     
            //service for swagger


            // service for services
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));
            builder.Services.AddSingleton<IApplicationEmailSender, ApplicationEmailSender>();
            builder.Services.Configure<TokenConfiguration>(builder.Configuration.GetSection("TokenConfiguration"));
            builder.Services.AddSingleton<ITokenHandler, Utility.TokenHandler>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddSingleton<IApplicationEmailSender, ApplicationEmailSender>();



            //service for services

            //jwt
            builder.Services.AddAuthentication(optiones =>
            {
                optiones.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                optiones.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                optiones.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(o =>
                  o.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidAudience = builder.Configuration["TokenConfiguration:Audience"],
                      ValidIssuer = builder.Configuration["TokenConfiguration:Issuer"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenConfiguration:SecretKey"]!)),
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateIssuerSigningKey = true,
                      ValidateLifetime = true,
                      ClockSkew = TimeSpan.Zero,
                  }
              );

            //jwt

            // server for utitly 
            //server for utility




            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            var app = builder.Build();

            // Configure the HTTP request pipeline.
          
            
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
