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
    public static class EPPaciente
    {
        public static void RegistrarEndpointsPacientes(this IEndpointRouteBuilder rotas)
        {
            RouteGroupBuilder rotaPaciente = rotas.MapGroup("/pacientes").WithTags("Pacientes");

            rotaPaciente.MapGet("/", (AppDbContext dbContext) =>
            {
                var pacientes = dbContext.Pacientes
                .Include(p => p.Setor)
                .ToList();

                return TypedResults.Ok(pacientes);
            });

            rotaPaciente.MapPost("/", (AppDbContext dbContext, Paciente paciente) =>
            {
                if (string.IsNullOrWhiteSpace(paciente.Nome))
                {
                    return Results.BadRequest("Não é possível adicionar um paciente sem nome.");
                }

                try
                {
                    dbContext.Pacientes.Add(paciente);
                    dbContext.SaveChanges();

                    return TypedResults.Created($"/pacientes/{paciente.Id}", paciente);
                } catch (Exception ex)
                {
                    return Results.Problem($"Ocorreu um erro ao adicionar o paciente: {ex.Message}");
                }
            });

            rotaPaciente.MapPut("/{Id}", (AppDbContext dbContext, [FromRoute] int Id, Paciente paciente) =>
            {
                if (string.IsNullOrWhiteSpace(paciente.Nome))
                {
                    return Results.BadRequest("Não é possível alterar para um nome vazio.");
                }

                Paciente? encontrada = dbContext.Pacientes.Find(Id);

                if (encontrada is null)
                {
                    return Results.NotFound();
                }

                try
                {
                    paciente.Id = Id;

                    dbContext.Entry(encontrada)
                        .CurrentValues
                        .SetValues(paciente);

                    dbContext.SaveChanges();

                    return TypedResults.NoContent();
                } catch (Exception ex)
                {
                    return Results.Problem($"Ocorreu um erro ao atualizar o paciente: {ex.Message}");
                }
            });

            rotaPaciente.MapDelete("/{Id}", (AppDbContext dbContext, [FromRoute] int Id) =>
            {
                Paciente? encontrada = dbContext.Pacientes.Find(Id);

                if (encontrada is null)
                {
                    return Results.NotFound();
                }

                dbContext.Pacientes.Remove(encontrada);
                dbContext.SaveChanges();

                return TypedResults.NoContent();
            });
        }
    }
}