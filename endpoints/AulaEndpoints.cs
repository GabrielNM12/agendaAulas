using agendaAulas.utils;
using agendaAulas.models;
using Microsoft.EntityFrameworkCore;
using agendaAulas.services;

namespace agendaAulas.endpoints;

public static class AulaEndpoints {

    public static void MapAulaEndpoints(this WebApplication app)
    {
        app.MapGet("/get/aulas", async (AppDbContext db) =>
        {
            return await UtilHandlers.SafeExecuteAsync(async () =>
            {
                var aulas = await db.Aulas
                                    .Include(a => a.TipoAula)
                                    .Select(a => new
                                    {
                                        a.Id,
                                        TipoAula = a.TipoAula.Nome,
                                        DataHora = a.DataHora.ToString("dd/MM/yyyy HH:mm"),
                                        a.CapacidadeMax
                                    })
                                    .ToListAsync();
                return Results.Ok(aulas);
            });
        })
        .WithName("GetAulas")
        .WithOpenApi();

        app.MapGet("/get/aulas/{id}", async (int id, AppDbContext db) =>
        {
            return await UtilHandlers.SafeExecuteAsync(async () =>
            {
                var aulas = await db.Aulas
                                    .Include(a => a.TipoAula)
                                    .Where(a => a.Id == id)
                                    .Select(a => new
                                    {
                                        a.Id,
                                        TipoAula = a.TipoAula.Nome,
                                        DataHora = a.DataHora.ToString("dd/MM/yyyy HH:mm"),
                                        a.CapacidadeMax
                                    })
                                    .FirstOrDefaultAsync();

                if (aulas == null)
                {
                    throw new Exception("Aula não encontrada");
                }

                return Results.Ok(aulas);
            });
        })
        .WithName("GetAulaById")
        .WithOpenApi();

        app.MapPost("/insert/aulas", async (AppDbContext db, Aula aulaInput) =>
        {
            return await UtilHandlers.SafeExecuteAsync(async () =>
            {
                await db.Aulas.AddAsync(aulaInput);
                await db.SaveChangesAsync();
                return Results.Created($"/get/aulas/{aulaInput.Id}", aulaInput);
            });
        })
        .WithName("InsertAulas")
        .WithOpenApi();

        app.MapDelete("/delete/aulas/{id}", async (AulaService service, int id) =>
        {
            return await UtilHandlers.SafeExecuteAsync(async () =>
            {
                var result = await service.DeleteAula(id);

                return Results.Ok($"Aula removida com sucesso");
            });
        })
        .WithName("DeleteAulaById")
        .WithOpenApi();
    }
}