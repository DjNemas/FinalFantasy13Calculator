global using RestAPI.Database;
global using RestAPI.Database.Models;
global using RestAPI.Interfaces;
global using RestAPI.Services;
global using System.ComponentModel.DataAnnotations;
global using Microsoft.EntityFrameworkCore;
global using System.Text;
global using RestAPI.Utils;
global using Shared.DTOs;
global using Shared.Enums;
using Microsoft.OpenApi.Models;
using RestAPI.Middlewares;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;

namespace RestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);      

            builder.Services.AddDbContext<FFXIIIDbContext>(options =>
            {
#if DEBUG
                options.UseSqlServer(builder.Configuration.GetConnectionString("Debug"));
#elif RELEASE
                options.UseSqlServer(builder.Configuration.GetConnectionString("Release"));
#endif
            });

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                // Add Bearer Token Authentication to Swagger UI
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Description = "Bearer Token",
                    Scheme = "Bearer"
                });

                // Addition Nuget for Swagger UI to enable Bearer Token input for Endpoints with Authorize Attribute
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IAccessoireService, AccessoireService>();
            builder.Services.AddScoped<IUserService, UserService>();

            var app = builder.Build();

            app.UseMiddleware<AuthorizeMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
