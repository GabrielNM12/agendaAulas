using agendaAulas.models;
using agendaAulas.enums;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Aula> Aulas => Set<Aula>();
    public DbSet<Aluno> Alunos => Set<Aluno>();
    public DbSet<TipoAula> TipoAulas => Set<TipoAula>();
    public DbSet<Agendamento> Agendamentos => Set<Agendamento>();

    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /* Dados Iniciais para a DEMO */
        modelBuilder.Entity<TipoAula>().HasData(
            new TipoAula { Id = 1, Nome = "Pilates" },
            new TipoAula { Id = 2, Nome = "Cross" },
            new TipoAula { Id = 3, Nome = "Funcional" }
        );

        modelBuilder.Entity<Aula>().HasData(
            new Aula { Id = 1, TipoAulaId = 1, DataHora = new DateTimeOffset(2025, 09, 15, 09, 00, 00, TimeSpan.Zero), CapacidadeMax = 10 },
            new Aula { Id = 2, TipoAulaId = 1, DataHora = new DateTimeOffset(2025, 09, 16, 10, 00, 00, TimeSpan.Zero), CapacidadeMax = 12 },
            new Aula { Id = 3, TipoAulaId = 1, DataHora = new DateTimeOffset(2025, 09, 17, 11, 00, 00, TimeSpan.Zero), CapacidadeMax = 15 },
            new Aula { Id = 4, TipoAulaId = 1, DataHora = new DateTimeOffset(2025, 09, 18, 12, 00, 00, TimeSpan.Zero), CapacidadeMax = 8 },
            new Aula { Id = 5, TipoAulaId = 2, DataHora = new DateTimeOffset(2025, 09, 19, 13, 00, 00, TimeSpan.Zero), CapacidadeMax = 20 },
            new Aula { Id = 6, TipoAulaId = 2, DataHora = new DateTimeOffset(2025, 09, 15, 14, 00, 00, TimeSpan.Zero), CapacidadeMax = 10 },
            new Aula { Id = 7, TipoAulaId = 2, DataHora = new DateTimeOffset(2025, 09, 16, 15, 00, 00, TimeSpan.Zero), CapacidadeMax = 18 },
            new Aula { Id = 8, TipoAulaId = 3, DataHora = new DateTimeOffset(2025, 09, 17, 16, 00, 00, TimeSpan.Zero), CapacidadeMax = 12 },
            new Aula { Id = 9, TipoAulaId = 3, DataHora = new DateTimeOffset(2025, 09, 18, 17, 00, 00, TimeSpan.Zero), CapacidadeMax = 14 },
            new Aula { Id = 10, TipoAulaId = 3, DataHora = new DateTimeOffset(2025, 09, 19, 18, 00, 00, TimeSpan.Zero), CapacidadeMax = 16 }
        );

        modelBuilder.Entity<Aluno>().HasData(
            new Aluno { Id = 1, Nome = "Ana Silva", TipoPlano = TipoPlano.Mensal },
            new Aluno { Id = 2, Nome = "Bruno Costa", TipoPlano = TipoPlano.Trimestral },
            new Aluno { Id = 3, Nome = "Carla Souza", TipoPlano = TipoPlano.Anual },
            new Aluno { Id = 4, Nome = "Diego Rocha", TipoPlano = TipoPlano.Mensal },
            new Aluno { Id = 5, Nome = "Eduarda Lima", TipoPlano = TipoPlano.Trimestral },
            new Aluno { Id = 6, Nome = "Felipe Martins", TipoPlano = TipoPlano.Anual },
            new Aluno { Id = 7, Nome = "Gabriela Almeida", TipoPlano = TipoPlano.Mensal },
            new Aluno { Id = 8, Nome = "Henrique Santos", TipoPlano = TipoPlano.Trimestral },
            new Aluno { Id = 9, Nome = "Isabela Nunes", TipoPlano = TipoPlano.Anual },
            new Aluno { Id = 10, Nome = "João Pedro", TipoPlano = TipoPlano.Mensal }
        );

        modelBuilder.Entity<Agendamento>().HasData(
            new Agendamento { Id = 1, AlunoId = 1, AulaId = 1 },
            new Agendamento { Id = 2, AlunoId = 2, AulaId = 2 },
            new Agendamento { Id = 3, AlunoId = 3, AulaId = 3 },
            new Agendamento { Id = 4, AlunoId = 4, AulaId = 4 },
            new Agendamento { Id = 5, AlunoId = 5, AulaId = 5 },
            new Agendamento { Id = 6, AlunoId = 6, AulaId = 6 },
            new Agendamento { Id = 7, AlunoId = 7, AulaId = 7 },
            new Agendamento { Id = 8, AlunoId = 8, AulaId = 8 },
            new Agendamento { Id = 9, AlunoId = 9, AulaId = 9 },
            new Agendamento { Id = 10, AlunoId = 10, AulaId = 10 }
        );
    }
}
