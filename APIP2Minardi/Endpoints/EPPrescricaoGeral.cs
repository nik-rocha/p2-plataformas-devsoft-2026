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
    public static class EPrescricaoGeral
    {
        public static void RegistrarEndpointsPrescricoesGerais(this IEndpointRouteBuilder rotas)
        {
            RouteGroupBuilder rotaPrescricaoGeral = rotas.MapGroup("/prescricoes-gerais").WithTags("Precrições Gerais");

            rotaPrescricaoGeral.MapGet("/", (AppDbContext dbContext) =>
            {
                var prescricoesGerais = dbContext.PrescricoesGerais
                .Include(p => p.Paciente)
                .Include(p => p.Medico)
                .ToList();
                return TypedResults.Ok(prescricoesGerais);
            });

            rotaPrescricaoGeral.MapPost("/", (AppDbContext dbContext, PrescricaoGeral precricaoGeral) =>
            {
                try
                {
                    dbContext.PrescricoesGerais.Add(precricaoGeral);
                    dbContext.SaveChanges();

                    return TypedResults.Created($"/prescricoes-gerais/{precricaoGeral.Id}", precricaoGeral);
                } catch (Exception ex)
                {
                    return Results.Problem($"Ocorreu um erro ao adicionar a prescrição geral: {ex.Message}");
                }
            });

            rotaPrescricaoGeral.MapPut("/{Id}", (AppDbContext dbContext, [FromRoute] int Id, PrescricaoGeral prescricaoGeral) =>
            {
                PrescricaoGeral? encontrada = dbContext.PrescricoesGerais.Find(Id);

                if (encontrada is null)
                {
                    return Results.NotFound();
                }

                try
                {
                    prescricaoGeral.Id = Id;

                    dbContext.Entry(encontrada)
                        .CurrentValues
                        .SetValues(prescricaoGeral);

                    dbContext.SaveChanges();
                    return TypedResults.NoContent();
                } catch (Exception ex)
                {
                    return Results.Problem($"Ocorreu um erro ao atualizar a prescrição geral: {ex.Message}");
                }
            });

            rotaPrescricaoGeral.MapDelete("/{Id}", (AppDbContext dbContext, [FromRoute] int Id) =>
            {
                PrescricaoGeral? encontrada = dbContext.PrescricoesGerais.Find(Id);

                if (encontrada is null)
                {
                    return Results.NotFound();
                }

                dbContext.PrescricoesGerais.Remove(encontrada);
                dbContext.SaveChanges();

                return TypedResults.NoContent();
            });
        }
    }
}