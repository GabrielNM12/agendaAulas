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
}
