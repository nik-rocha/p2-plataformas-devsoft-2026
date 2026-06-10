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
            RouteGroupBuilder rotaPrescricaoGeral = rotas.MapGroup("/prescricoes-gerais");

            rotaPrescricaoGeral.MapGet("/", (AppDbContext dbContext) =>
            {
                var prescricoesGerais = dbContext.PrescricoesGerais
                .Include(p => p.Paciente)
                .Include(p => p.Medico)
                .ToList();
                return TypedResults.Ok(prescricoesGerais);
            });
        }
    }
}