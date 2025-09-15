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
                                                DataHora = a.Aula.DataHora.ToString("dd/MM/yyyy HH:mm")
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
                                                DataHora = a.Aula.DataHora.ToString("dd/MM/yyyy HH:mm")
                                            })
                                            .FirstOrDefaultAsync();

                if (agendamentos == null)
                {
                    throw new Exception("Agendamento não encontrado");
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

        app.MapDelete("/delete/agendamentos/{id}", async (int id, AppDbContext db) =>
        {
            return await UtilHandlers.SafeExecuteAsync(async () =>
            {
                var agendamentos = await db.Agendamentos
                                            .Include(a => a.Aluno)
                                            .Include(a => a.Aula)
                                            .Include(a => a.Aula.TipoAula)
                                            .Where(a => a.Id == id)
                                            .FirstOrDefaultAsync();

                if (agendamentos == null)
                {
                    throw new Exception("Agendamento não encontrado");
                }

                var agendamentoAluno    = agendamentos.Aluno.Nome;
                var agendamentoAula     = agendamentos.Aula.TipoAula.Nome;
                var agendamentoDataHora = agendamentos.Aula.DataHora.ToString("dd/MM/yyyy HH:mm");

                db.Agendamentos.Remove(agendamentos);
                await db.SaveChangesAsync();

                return Results.Ok($"Agendamento do aluno {agendamentoAluno} na aula {agendamentoAula} no dia e horário {agendamentoDataHora} removido com sucesso");
            });
        })
        .WithName("DeleteAgendamentoById")
        .WithOpenApi();
    }
}