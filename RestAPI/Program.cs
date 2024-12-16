using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RestAPI.Database;
using RestAPI.Interfaces;
using RestAPI.Middlewares;
using RestAPI.Services;
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

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
                

            //builder.Services.AddOpenApi();

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
                        
            builder.Services.AddScoped<IDatabaseService, DatabaseService>();

            var app = builder.Build();

            app.UseMiddleware<AuthorizeMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
