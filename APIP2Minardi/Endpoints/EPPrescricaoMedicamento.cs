using APIP2Minardi.Data;
using APIP2Minardi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIP2Minardi.Endpoints
{
    public static class EPrescricaoMedicamento
    {
        public static void RegistrarEndpointsPrescricoesMedicamentos(this IEndpointRouteBuilder rotas)
        {
            RouteGroupBuilder rotaPrescricaoMedicamento = rotas.MapGroup("/prescricoes-medicamentos").WithTags("Precrições dos Medicamentos"); ;

            rotaPrescricaoMedicamento.MapGet("/", (AppDbContext dbContext) =>
            {
                var prescricoesMedicamentos = dbContext.PrescricoesMedicamentos
                .Include(p => p.PrescricaoGeral)
                .Include(p => p.Medicamentos)
                .ToList();

                return TypedResults.Ok(prescricoesMedicamentos);
            });

            rotaPrescricaoMedicamento.MapPost("/", (AppDbContext dbContext, PrescricaoMedicamento precricaoMedicamento) =>
            {
                try
                {
                    dbContext.PrescricoesMedicamentos.Add(precricaoMedicamento);
                    dbContext.SaveChanges();

                    return TypedResults.Created($"/prescricoes-medicamentos/{precricaoMedicamento.Id}", precricaoMedicamento);
                } catch (Exception ex)
                {
                    return Results.Problem($"Ocorreu um erro ao adicionar a prescrição do medicamento: {ex.Message}");
                }
            });

            rotaPrescricaoMedicamento.MapPut("/{Id}", (AppDbContext dbContext, [FromRoute] int Id, PrescricaoMedicamento prescricaoMedicamento) =>
            {
                PrescricaoMedicamento? encontrada = dbContext.PrescricoesMedicamentos.Find(Id);

                if (encontrada is null)
                {
                    return Results.NotFound();
                }

                try
                {
                    prescricaoMedicamento.Id = Id;

                    dbContext.Entry(encontrada)
                        .CurrentValues
                        .SetValues(prescricaoMedicamento);

                    dbContext.SaveChanges();

                    return TypedResults.NoContent();
                } catch (Exception ex)
                {
                    return Results.Problem($"Ocorreu um erro ao atualizar a prescrição do medicamento: {ex.Message}");
                }
            });

            rotaPrescricaoMedicamento.MapDelete("/{Id}", (AppDbContext dbContext, [FromRoute] int Id) =>
            {
                PrescricaoMedicamento? encontrada = dbContext.PrescricoesMedicamentos.Find(Id);

                if (encontrada is null)
                {
                    return Results.NotFound();
                }

                dbContext.PrescricoesMedicamentos.Remove(encontrada);
                dbContext.SaveChanges();

                return TypedResults.NoContent();
            });
        }
    }
}