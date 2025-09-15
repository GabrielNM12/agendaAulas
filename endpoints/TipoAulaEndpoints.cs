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
                    throw new Exception("Tipo de aula não encontrada");
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

        app.MapDelete("/delete/tipoaula/{id}", async (int id, AppDbContext db) =>
        {
            return await UtilHandlers.SafeExecuteAsync(async () =>
            {
                var tipoAula = await db.TipoAulas
                                       .Where(a => a.Id == id)
                                       .FirstOrDefaultAsync();

                if (tipoAula == null)
                {
                    throw new Exception("Tipo de aula não encontrada");
                }

                var aula = await db.Aulas
                                   .Where(a => a.TipoAulaId == id)
                                   .FirstOrDefaultAsync();

                if (aula != null) {
                    throw new Exception("Tipo de aula possui aulas agendadas, não pode ser removido");
                }

                db.TipoAulas.Remove(tipoAula);
                await db.SaveChangesAsync();

                return Results.Ok( new { Message = "Tipo de aula removido com sucesso" });
            });
        })
        .WithName("DeleteTipoAulaById")
        .WithOpenApi();
    }
}