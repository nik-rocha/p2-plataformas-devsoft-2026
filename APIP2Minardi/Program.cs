using Microsoft.EntityFrameworkCore;
using APIP2Minardi.Data;

namespace APIP2Minardi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the cont ainer.
            builder.Services.AddAuthorization();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi(); // Gera o JSON do .NET em: /openapi/v1.json

                // Configura a interface antiga para ler o JSON novo
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "Minha API v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.Run();
        }
    }
}
