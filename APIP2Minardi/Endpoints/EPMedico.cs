using APIP2Minardi.Data;
using APIP2Minardi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIP2Minardi.Endpoints
{
    public static class EPMedico
    {
        public static void RegistrarEndpointsMedicos(this IEndpointRouteBuilder rotas)
        {
            RouteGroupBuilder rotaMedicos = rotas.MapGroup("/medicos");

            rotaMedicos.MapGet("/", (AppDbContext dbContext, [FromQuery] string? nome) =>
            {
                IEnumerable<Medico> medicosFiltrados = dbContext.Medicos.AsQueryable();

                if (!string.IsNullOrWhiteSpace(nome))
                {
                    medicosFiltrados = medicosFiltrados.Where(s => s.Nome == nome);
                }

                return TypedResults.Ok(medicosFiltrados);
            });

            rotaMedicos.MapPost("/", (AppDbContext dbContext, Medico medico) =>
            {
                if (string.IsNullOrWhiteSpace(medico.Nome))
                {
                    return Results.BadRequest("O campo 'Nome' é obrigatório.");
                }

                try
                {
                    var novoMedico = dbContext.Medicos.Add(medico);
                    dbContext.SaveChanges();
                    return TypedResults.Created($"/medicos/{medico.Id}", medico);
                } catch (Exception ex)
                {
                    return Results.Problem($"Ocorreu um erro ao adicionar um médico: {ex.Message}");
                }
            });

            rotaMedicos.MapPut("/{Id}", (AppDbContext dbContext, [FromRoute] int Id, Medico medico) =>
            {
                if (string.IsNullOrWhiteSpace(medico.Nome))
                {
                    return Results.BadRequest("Não é possível alterar para um nome vazio.");
                }

                Medico? medicoEncontrado = dbContext.Medicos.Find(Id);

                if (medicoEncontrado is null)
                {
                    return Results.NotFound();
                }

                try
                {
                    medico.Id = Id;
                    dbContext.Entry(medicoEncontrado).CurrentValues.SetValues(medico);
                    dbContext.SaveChanges();
                    return TypedResults.NoContent();
                } catch (Exception ex)
                {
                    return Results.Problem($"Ocorreu um erro ao atualizar o médico: {ex.Message}");
                }
            });

            rotaMedicos.MapDelete("/{Id}", (AppDbContext dbContext, [FromRoute] int Id) =>
            {
                Medico? medicoEncontrado = dbContext.Medicos.Find(Id);

                if (medicoEncontrado is null)
                {
                    return Results.NotFound();
                }

                dbContext.Medicos.Remove(medicoEncontrado);
                dbContext.SaveChanges();
                return TypedResults.NoContent();
            });
        }
    }
}