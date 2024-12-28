global using Homepage.Interfaces;
global using Homepage.Models.RestAPI;
global using Homepage.Service;
global using Shared.DTOs;
global using Shared.Enums;
using Homepage.Middleware;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Homepage
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAuthentication().AddBearerToken();
            builder.Services.AddSession();

            // Add services to the container.            builder
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetSection("RestAPI:Domain").Value!) });
            var test = builder.Configuration.GetSection("RestAPI:Domain").Value!;


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseSession();
            app.UseMiddleware<AuthorizeMiddleware>();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            await app.RunAsync();
        }
    }
}
