using Microsoft.EntityFrameworkCore;
using APIP2Minardi.Data;
using APIP2Minardi.Endpoints;

namespace APIP2Minardi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the cont ainer.
            builder.Services.AddAuthorization();

            builder.Services.AddDbContext<AppDbContext>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.RegistrarEndpointsSetores();
            app.RegistrarEndpointsPrescricoesMedicamentos();

            app.Run();
        }
    }
}
