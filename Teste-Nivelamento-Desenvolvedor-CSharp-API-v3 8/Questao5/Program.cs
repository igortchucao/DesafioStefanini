using MediatR;
using Questao5.Application.Commands;
using Questao5.Application.Handlers;
using Questao5.Application.Queries;
using Questao5.Domain.DTO;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.CommandStore;
using Questao5.Infrastructure.Database.QueryStore;
using Questao5.Infrastructure.Sqlite;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddSingleton<IHandlerAsync<MovimentacaoFinanceira, Guid>, MovimentacaoFinanceiraHandler>();
builder.Services.AddSingleton<IGerarMovimentacaoFinanceira, GerarMovimentacaoFinanceira>();
builder.Services.AddSingleton<IMovimentacaoFinanceiraRepository, MovimentacaoFinanceiraRepository>();

builder.Services.AddSingleton<IHandlerAsync<string, SaldoReturn>, ObtemSaldoHandler>();
builder.Services.AddSingleton<IObterSaldoConta, ObterSaldoConta>();
builder.Services.AddSingleton<IObterSaldoRepository, ObterSaldoRepository>();

builder.Services.AddSingleton<IContaCorrenteRepository, ContaCorrenteRepository>();

// sqlite
builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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


