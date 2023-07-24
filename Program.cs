using AppCRUD.Data;
using AppCRUD.Integracao;
using AppCRUD.Integracao.Interfaces;
using AppCRUD.Integracao.Refit;
using AppCRUD.Repositorios;
using AppCRUD.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using Refit;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SistemaTarefasDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TarefasDbContext")));

builder.Services.AddScoped<ITarefaRepositorio, TarefaRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IViaCepIntegracao, ViaCepIntegracao>();

builder.Services.AddRefitClient<IViaCepIntegracaoRefit>().ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri("https://viacep.com.br");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
