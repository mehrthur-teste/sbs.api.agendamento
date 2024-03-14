using AutoMapper;
using Microsoft.Extensions.Options;
using sbs.api.agendamento.dependencyinjection;
using sbs.api.agendamento.dominio.Interface;
using sbs.api.agendamento.dominio.Mapping;
using sbs.api.agendamento.repository.Configuration;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cosmosDbConfig = new DbConfiguration();
new ConfigureFromConfigurationOptions<DbConfiguration>(
    builder.Configuration.GetSection("DbConfiguration"))
        .Configure(cosmosDbConfig);
builder.Services.AddSingleton(cosmosDbConfig);

builder.Services.RegistrarDependencias(cosmosDbConfig);

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile<AutomapperConfiguration>();

});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

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

app.Run();
