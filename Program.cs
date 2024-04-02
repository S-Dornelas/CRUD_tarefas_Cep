using AppCRUD.Data;
using AppCRUD.Integracao;
using AppCRUD.Integracao.Interfaces;
using AppCRUD.Integracao.Refit;
using AppCRUD.Repositorios;
using AppCRUD.Repositorios.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Refit;
using System;
using System.Reflection;
using System.Text;

string chaveSecreta = "71cb0cdb-748d-495d-a5e6-cf2b6d864f19";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "siatema de Tarefas - API", Version = "v1" });

    var securitySchema = new OpenApiSecurityScheme
    {
        Name = "JWT Autenticação",
        Description = "Entre com o JWT Bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securitySchema);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securitySchema, new string[] {} }
    });
});
builder.Services.AddDbContext<SistemaTarefasDbContext>(options =>
                   options.UseNpgsql(builder.Configuration.GetConnectionString("TarefasDbContext")));

//builder.Services.AddDbContext<SistemaTarefasDbContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("TarefasDbContext")));

builder.Services.AddScoped<ITarefaRepositorio, TarefaRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IViaCepIntegracao, ViaCepIntegracao>();

builder.Services.AddRefitClient<IViaCepIntegracaoRefit>().ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri("https://viacep.com.br");
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,  
        ValidateAudience = true,
        ValidateLifetime = true, //validar o tempo de expiração - acabando o tempo ele reinicia
        ValidateIssuerSigningKey = true,
        ValidIssuer = "sua_empresa",
        ValidAudience = "sua_aplicacao",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta))
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
