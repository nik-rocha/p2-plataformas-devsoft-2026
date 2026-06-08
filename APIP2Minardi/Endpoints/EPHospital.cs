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
    public static class EPHospital
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
        }
    }
}