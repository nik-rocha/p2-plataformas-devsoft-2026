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
    public static class EPMedicamento
    {
        public static void RegistrarEndpointsMedicamentos(this IEndpointRouteBuilder rotas)
        {
            RouteGroupBuilder rotaMedicamentos = rotas.MapGroup("/medicamentos");

            rotaMedicamentos.MapGet("/", (AppDbContext dbContext, [FromQuery] string? nome) =>
            {
                IEnumerable<Medicamento> medicamentosFiltrados = dbContext.Medicamentos.AsQueryable();

                if (!string.IsNullOrWhiteSpace(nome))
                {
                    medicamentosFiltrados = medicamentosFiltrados.Where(s => s.Nome == nome);
                }

                return TypedResults.Ok(medicamentosFiltrados);
            });

            rotaMedicamentos.MapPost("/", (AppDbContext dbContext, Medicamento medicamento) =>
            {
                if (string.IsNullOrWhiteSpace(medicamento.Nome))
                {
                    return Results.BadRequest("Não é possível adicionar um remédio sem nome.");
                }

                try
                {
                    var novoMedicamento = dbContext.Medicamentos.Add(medicamento);
                    dbContext.SaveChanges();
                    return TypedResults.Created($"/medicamentos/{medicamento.Id}", medicamento);
                } catch (Exception ex)
                {
                    return Results.Problem($"Ocorreu um erro ao adicionar o medicamento: {ex.Message}");
                }
            });

            rotaMedicamentos.MapPut("/{Id}", (AppDbContext dbContext, [FromRoute] int Id, Medicamento medicamento) =>
            {
                if (string.IsNullOrWhiteSpace(medicamento.Nome))
                {
                    return Results.BadRequest("Não é possível alterar para um nome vazio.");
                }

                Medicamento? medicamentoEncontrado = dbContext.Medicamentos.Find(Id);

                if (medicamentoEncontrado is null)
                {
                    return Results.NotFound();
                }

                try
                {
                    medicamento.Id = Id;
                    dbContext.Entry(medicamentoEncontrado).CurrentValues.SetValues(medicamento);
                    dbContext.SaveChanges();
                    return TypedResults.NoContent();
                } catch (Exception ex)
                {
                    return Results.Problem($"Ocorreu um erro ao atualizar o medicamento: {ex.Message}");
                }
            });

            rotaMedicamentos.MapDelete("/{Id}", (AppDbContext dbContext, [FromRoute] int Id) =>
            {
                Medicamento? medicamentoEncontrado = dbContext.Medicamentos.Find(Id);

                if (medicamentoEncontrado is null)
                {
                    return Results.NotFound();
                }

                dbContext.Medicamentos.Remove(medicamentoEncontrado);
                dbContext.SaveChanges();
                return TypedResults.NoContent();
            });
        }
    }
}