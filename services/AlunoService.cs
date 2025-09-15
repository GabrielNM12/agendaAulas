using agendaAulas.models;
using Microsoft.EntityFrameworkCore;

namespace agendaAulas.services;

public class AlunoService
{
    private readonly AppDbContext _db;

    public AlunoService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Aluno> InsertAluno(Aluno alunoInput) {
        
        if (_db.Alunos.Any(a => a.Nome == alunoInput.Nome))
            throw new Exception("Aluno com mesmo nome já existente");

        await _db.Alunos.AddAsync(alunoInput);
        await _db.SaveChangesAsync();
        
        return alunoInput;
    }

    public async Task<Aluno> DeleteAluno(int id) {
        
        var aluno = await _db.Alunos.FindAsync(id);
        if (aluno == null)
            throw new Exception("Aluno não encontrado");

        var agendamento = await _db.Agendamentos
                                   .Include(a => a.Aula)
                                   .Where(a => a.AlunoId == id && a.Aula.DataHora > DateTime.Now)
                                   .FirstOrDefaultAsync();
        if (agendamento != null)
            throw new Exception("Aluno possui agendamentos pendentes, não pode ser removido");

        _db.Alunos.Remove(aluno);
        await _db.SaveChangesAsync();

        return aluno;
    }
}
