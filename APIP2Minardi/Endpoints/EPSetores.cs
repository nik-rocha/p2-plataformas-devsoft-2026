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
    public static class EPSetores
    {
        public static void RegistrarEndpointsSetores(this IEndpointRouteBuilder rotas)
        {
            RouteGroupBuilder rotaSetores = rotas.MapGroup("/setores");

            rotaSetores.MapGet("/", (AppDbContext dbContext, [FromQuery] string? andar) =>
            {
                IEnumerable<Setor> setoresFiltrados = dbContext.Setores.AsQueryable();

                if (!string.IsNullOrWhiteSpace(andar))
                {
                    setoresFiltrados = setoresFiltrados.Where(s => s.Andar == andar);
                }

                return TypedResults.Ok(setoresFiltrados);
            });

            rotaSetores.MapPost("/", (AppDbContext dbContext, Setor setor) =>
            {
                if (string.IsNullOrWhiteSpace(setor.Nome))
                {
                    return Results.BadRequest("Não é possível adicionar um setor sem nome.");
                }

                try
                {
                    var novoSetor = dbContext.Setores.Add(setor);
                    dbContext.SaveChanges();
                    return TypedResults.Created($"/setores/{setor.Id}", setor);
                } catch (Exception ex)
                {
                    return Results.Problem($"Ocorreu um erro ao adicionar o setor: {ex.Message}");
                }
            });

            rotaSetores.MapPut("/{Id}", (AppDbContext dbContext, [FromRoute] int Id, Setor setor) =>
            {
                if (string.IsNullOrWhiteSpace(setor.Nome))
                {
                    return Results.BadRequest("Não é possível alterar para um nome vazio.");
                }

                Setor? setorEncontrado = dbContext.Setores.Find(Id);

                if (setorEncontrado is null)
                {
                    return Results.NotFound();
                }

                try
                {
                    setor.Id = Id;
                    dbContext.Entry(setorEncontrado).CurrentValues.SetValues(setor);
                    dbContext.SaveChanges();
                    return TypedResults.NoContent();
                } catch (Exception ex)
                {
                    return Results.Problem($"Ocorreu um erro ao atualizar o setor: {ex.Message}");
                }
            });

            rotaSetores.MapDelete("/{Id}", (AppDbContext dbContext, [FromRoute] int Id) =>
            {
                Setor? setorEncontrado = dbContext.Setores.Find(Id);

                if (setorEncontrado is null)
                {
                    return Results.NotFound();
                }

                dbContext.Setores.Remove(setorEncontrado);
                dbContext.SaveChanges();
                return TypedResults.NoContent();
            });
        }
    }
}