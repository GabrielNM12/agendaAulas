using Microsoft.EntityFrameworkCore;

namespace agendaAulas.utils;

public static class UtilHandlers {
    public static async Task<IResult> SafeExecuteAsync(Func<Task<IResult>> func)
    {
        try
        {
            var result = await func();

            return result;
        }
        catch (DbUpdateException ex)
        {

            return Results.Conflict(new {
                error      = true,
                message    = "Erro ao adicionar novo registro",
                detail     = ex.Message,
            });
        }
        catch (Exception ex)
        {

            return Results.Conflict(new {
                error      = true,
                message    = "Ocorreu algum erro na requisição",
                detail     = ex.Message,
            });
        }
    }
}