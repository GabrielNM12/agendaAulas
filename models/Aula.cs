using Microsoft.EntityFrameworkCore;

namespace agendaAulas.models;

public class Aula
{
    public int Id { get; set; }
    public int TipoAulaId { get; set; }
    public TipoAula TipoAula { get; set; } = default!;
    public DateTimeOffset DataHora { get; set; }
    public int CapacidadeMax { get; set; }

    public int CountAgendamentos(AppDbContext db)
    {

        var now = DateTimeOffset.UtcNow;
        return db.Agendamentos
                 .Include(a => a.Aula)
                 .Count(a => a.AulaId == this.Id && a.Aula.DataHora <= now);
    }
}