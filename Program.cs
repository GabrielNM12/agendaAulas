using agendaAulas.services;
using agendaAulas.endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<AlunoService>();
builder.Services.AddScoped<AgendamentoService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapAulaEndpoints();
app.MapTipoAulaEndpoints();
app.MapAlunoEndpoints();
app.MapAgendamentoEndpoints();

app.MapControllers();

app.Run();