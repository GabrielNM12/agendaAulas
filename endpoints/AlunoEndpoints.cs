using agendaAulas.utils;
using agendaAulas.models;
using agendaAulas.services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace agendaAulas.endpoints;

public static class AlunoEndpoints
{

    public static void MapAlunoEndpoints(this WebApplication app)
    {

        app.MapGet("/get/alunos", async (AppDbContext db) =>
        {
            return await UtilHandlers.SafeExecuteAsync(async () =>
            {
                var alunos = await db.Alunos
                                    .Select(a => new
                                    {
                                        a.Id,
                                        a.Nome,
                                        TipoPlano = a.TipoPlano.ToString()
                                    })
                                    .ToListAsync();
                return Results.Ok(alunos);
            });
        })
        .WithName("GetAlunos")
        .WithOpenApi();

        app.MapGet("/get/alunos/{id}", async (int id, AppDbContext db) =>
        {
            return await UtilHandlers.SafeExecuteAsync(async () =>
            {
                var alunos = await db.Alunos
                                    .Where(a => a.Id == id)
                                    .Select(a => new
                                    {
                                        a.Id,
                                        a.Nome,
                                        TipoPlano = a.TipoPlano.ToString()
                                    })
                                    .FirstOrDefaultAsync();

                if (alunos == null)
                {
                    return Results.NotFound(new { Message = "Aluno não encontrado" });
                }

                return Results.Ok(alunos);
            });
        })
        .WithName("GetAlunoById")
        .WithOpenApi();

        app.MapPost("/insert/alunos", async (AlunoService service, [FromBody] Aluno alunoInput) =>
        {
            return await UtilHandlers.SafeExecuteAsync(async () =>
            {
                var result = await service.InsertAluno(alunoInput);
                return Results.Created($"/get/alunos/{result.Id}", result);
            });
        })
        .WithName("InsertAlunos")
        .WithOpenApi();

        app.MapGet("/reports/alunos/{id}", async (int id, AppDbContext db) =>
        {
            return await UtilHandlers.SafeExecuteAsync(async () =>
            {

                var aluno = await db.Alunos.FindAsync(id);

                if (aluno == null) throw new Exception("Aluno não encontrado");

                var CurrentMonth = DateTimeOffset.Now.Month;
                var CountAgendamentosCurrentMonth = await db.Agendamentos
                                                            .Include(a => a.Aula)
                                                            .CountAsync(a => a.AlunoId == id && a.Aula.DataHora.Month == CurrentMonth) ;

                var aulasFrequentes = await db.Agendamentos
                                              .Include(a => a.Aula)
                                              .Include(a => a.Aula.TipoAula)
                                              .Include(a => a.Aluno)
                                              .Where(a => a.AulaId  == a.Aula.Id &&
                                                          a.AlunoId == a.Aluno.Id)
                                              .GroupBy(a => a.Aula.TipoAula.Nome)
                                              .Select(a => new 
                                              {
                                                  TipoAula = a.Key,
                                                  QuantidadeAgendamentos = a.Count(),
                                              })
                                              .OrderByDescending(a => a.QuantidadeAgendamentos)
                                              .ToListAsync();

                return Results.Ok(new {
                    aluno.Nome,
                    TipoPlano = aluno.TipoPlano.ToString(),
                    AgendamentosCurrentMonth = CountAgendamentosCurrentMonth,
                    aulasFrequentes,
                });
            });
        })
        .WithName("ReportAluno")
        .WithOpenApi();
    }
}