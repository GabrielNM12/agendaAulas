using agendaAulas.enums;
using Microsoft.EntityFrameworkCore;

namespace agendaAulas.models;

public class Aluno
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public TipoPlano TipoPlano { get; set; }

    public int GetMaxAulas()
    {
        return TipoPlano switch
        {
            TipoPlano.Mensal => 12,
            TipoPlano.Trimestral => 20,
            TipoPlano.Anual => 30,
            _ => 0
        };
    }

    public int CountAgendamentos(AppDbContext db)
    {

        var now = DateTimeOffset.UtcNow;

        return db.Agendamentos
                 .Include(a => a.Aula)
                 .Count(a => a.AlunoId == this.Id && a.Aula.DataHora <= now) ;
    }
}
