using agendaAulas.utils;
using agendaAulas.models;
using Microsoft.EntityFrameworkCore;

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
                                        a.DataHora,
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
                                        a.DataHora,
                                        a.CapacidadeMax
                                    })
                                    .FirstOrDefaultAsync();

                if (aulas == null)
                {
                    return Results.NotFound( new { Message = "Aula não encontrada" });
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
    }
}