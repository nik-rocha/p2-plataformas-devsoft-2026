using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v3", new OpenApiInfo
                {
                    Version = "v3",
                    Title = "API P2 Minardi - Clínica Médica",
                    Description = "API para gerenciamento de setores, médicos, pacientes e prescrições em uma clínica médica."
                });
            });

            builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v3/swagger.json", "API P2 Minardi - Clínica Médica v3");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.RegistrarEndpointsSetores();
            app.RegistrarEndpointsPrescricoesMedicamentos();
            app.RegistrarEndpointsPrescricoesGerais();
            app.RegistrarEndpointsPacientes();
            app.RegistrarEndpointsMedicos();
            app.RegistrarEndpointsMedicamentos();

            app.Run();
        }
    }
}
