using MediatR;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Abstraction;
using Questao5.Filters;
using Questao5.Infrastructure.Database.QueryStore;
using Questao5.Infrastructure.Database.Repository;
using Questao5.Infrastructure.Sqlite;
using Questao5.Middleware;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// sqlite
builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
builder.Services.AddScoped<IDbConnection>(sp =>new SqliteConnection(builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite")));
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();

//Injection Dependecy
builder.Services.AddScoped<ICurrentAccountQueryStore, CurrentAccountQueryStore>();
builder.Services.AddScoped<IMovimentoRepository, MovimentoRepository>();
builder.Services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();
builder.Services.AddScoped<IIdempotenciaRepository, IdempotenciaRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options =>
{
    options.Filters.Add<CustomExceptionFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<IdempotencyMiddleware>();
app.UseMiddleware<ResponseCaptureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// sqlite
#pragma warning disable CS8602 // Dereference of a possibly null reference.
app.Services.GetService<IDatabaseBootstrap>().Setup();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

app.Run();

// Informações úteis:
// Tipos do Sqlite - https://www.sqlite.org/datatype3.html


