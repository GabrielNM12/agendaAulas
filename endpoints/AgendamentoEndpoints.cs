using agendaAulas.utils;
using agendaAulas.models;
using agendaAulas.services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace agendaAulas.endpoints;

public static class AgendamentoEndpoints
{
    public static void MapAgendamentoEndpoints(this WebApplication app)
    {
        app.MapGet("/get/agendamentos", async (AppDbContext db) =>
        {

            return await UtilHandlers.SafeExecuteAsync(async () =>
            {
                var agendamentos = await db.Agendamentos
                                            .Include(a => a.Aluno)
                                            .Include(a => a.Aula)
                                            .Include(a => a.Aula.TipoAula)
                                            .Select(a => new
                                            {
                                                a.Id,
                                                Aluno = a.Aluno.Nome,
                                                TipoAula = a.Aula.TipoAula.Nome,
                                                a.Aula.DataHora
                                            })
                                            .ToListAsync();

                return Results.Ok(agendamentos);
            });
        })
        .WithName("GetAgendamentos")
        .WithOpenApi();

        app.MapGet("/get/agendamentos/{id}", async (int id, AppDbContext db) =>
        {
            return await UtilHandlers.SafeExecuteAsync(async () =>
            {
                var agendamentos = await db.Agendamentos
                                            .Include(a => a.Aluno)
                                            .Include(a => a.Aula)
                                            .Include(a => a.Aula.TipoAula)
                                            .Where(a => a.Id == id)
                                            .Select(a => new
                                            {
                                                a.Id,
                                                Aluno = a.Aluno.Nome,
                                                TipoAula = a.Aula.TipoAula.Nome,
                                                a.Aula.DataHora
                                            })
                                            .FirstOrDefaultAsync();

                if (agendamentos == null)
                {
                    return Results.NotFound(new { Message = "Agendamento não encontrado" });
                }

                return Results.Ok(agendamentos);
            });
        })
        .WithName("GetAgendamentoById")
        .WithOpenApi();

        app.MapPost("/insert/agendamento", async (AgendamentoService service, [FromBody] Agendamento agendamentoInput) =>
        {
            return await UtilHandlers.SafeExecuteAsync(async () =>
            {
                var result = await service.InsertAgendamento(agendamentoInput);
                return Results.Created($"/get/agendamento/{result.Id}", result);
            });
        })
        .WithName("InsertAgendamento")
        .WithOpenApi();
    }
}