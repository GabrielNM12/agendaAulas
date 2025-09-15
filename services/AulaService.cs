using agendaAulas.models;
using Microsoft.EntityFrameworkCore;

namespace agendaAulas.services;

public class AulaService
{
    private readonly AppDbContext _db;

    public AulaService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Aula> DeleteAula(int id) {
        
        var aula = await _db.Aulas.FindAsync(id);
        if (aula == null)
            throw new Exception("Aula não encontrada");

        var agendamento = await _db.Agendamentos
                                   .Where(a => a.AulaId == id && aula.DataHora > DateTime.Now)
                                   .FirstOrDefaultAsync();
        if (agendamento != null)
            throw new Exception("Aula possui agendamentos pendentes, não pode ser removida");

        _db.Aulas.Remove(aula);
        await _db.SaveChangesAsync();

        return aula;
    }
}
