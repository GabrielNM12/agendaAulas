using agendaAulas.utils;
using agendaAulas.models;
using Microsoft.EntityFrameworkCore;

namespace agendaAulas.endpoints;

public static class TipoAulaEndpoints {

    public static void MapTipoAulaEndpoints(this WebApplication app)
    {
        app.MapGet("/get/tipoaula", async (AppDbContext db) =>
        {
            return await UtilHandlers.SafeExecuteAsync(async () =>
            {
                var tipoAulas = await db.TipoAulas.ToListAsync();
                return Results.Ok(tipoAulas);
            });
        })
        .WithName("GetTipoAulas")
        .WithOpenApi();

        app.MapGet("/get/tipoaula/{id}", async (int id, AppDbContext db) =>
        {
            return await UtilHandlers.SafeExecuteAsync(async () =>
            {
                var tipoAula = await db.TipoAulas
                                    .Where(a => a.Id == id)
                                    .FirstOrDefaultAsync();

                if (tipoAula == null)
                {
                    return Results.NotFound( new { Message = "Tipo de aula não encontrada" });
                }

                return Results.Ok(tipoAula);
            });
        })
        .WithName("GetTipoAulaById")
        .WithOpenApi();

        app.MapPost("/insert/tipoaula", async (AppDbContext db, TipoAula tipoAulaInput) =>
        {
            return await UtilHandlers.SafeExecuteAsync(async () =>
            {
                await db.TipoAulas.AddAsync(tipoAulaInput);
                await db.SaveChangesAsync();
                return Results.Created($"/get/tipoaula/{tipoAulaInput.Id}", tipoAulaInput);
            });
        })
        .WithName("InsertTipoAula")
        .WithOpenApi();
    }
}