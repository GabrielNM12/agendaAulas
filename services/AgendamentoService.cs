using agendaAulas.models;

namespace agendaAulas.services;

public class AgendamentoService
{
    private readonly AppDbContext _db;

    public AgendamentoService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Agendamento> InsertAgendamento(Agendamento agendamentoInput)
    {
        var aula  = await _db.Aulas.FindAsync(agendamentoInput.AulaId);
        var aluno = await _db.Alunos.FindAsync(agendamentoInput.AlunoId);

        /* Validação se encontra as tabelas relacionadas */
        if (aula  == null) throw new Exception("Aula não encontrada");
        if (aluno == null) throw new Exception("Aluno não encontrado");

        if (aula.DataHora < DateTimeOffset.Now) 
            throw new Exception("Aula já ocorreu, não é possível agendar");


        if (_db.Agendamentos.Any(a => a.AlunoId == agendamentoInput.AlunoId && a.AulaId == agendamentoInput.AulaId))
            throw new Exception("Aluno já se encontra agendado para essa aula");

        if (aula.CountAgendamentos(_db) >= aula.CapacidadeMax)
            throw new Exception("Aula lotada");

        if (aluno.CountAgendamentos(_db) >= aluno.GetMaxAulas())
            throw new Exception("Máximo de aulas de aluno atingidos");

        await _db.Agendamentos.AddAsync(agendamentoInput);
        await _db.SaveChangesAsync();

        return agendamentoInput;
    }
}
