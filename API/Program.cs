using Application.Handlers;
using Application.Interfaces;
using HealthChecks.UI.Client;
using Infra.Interfaces;
using Infra.Repositories;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Registrar os repositórios e suas interfaces
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<ISessionTokenRepository, SessionTokenRepository>();
builder.Services.AddScoped<IUserCredentialsRepository, UserCredentialsRepository>();
builder.Services.AddScoped<IOtpRepository, OtpRepository>();

// Registrar os handlers e suas interfaces
builder.Services.AddScoped<IGetClientByIdHandler, GetClientByIdHandler>();
builder.Services.AddScoped<IUpdateClientHandler, UpdateClientHandler>();
builder.Services.AddScoped<ICreateClientHandler, CreateClientHandler>();

builder.Services.AddScoped<ICreateUserCredentialsHandler, CreateUserCredentialsHandler>();
builder.Services.AddScoped<ILoginHandler, LoginHandler>();
builder.Services.AddScoped<IValidateJwtTokenHandler, ValidateJwtTokenHandler>();

builder.Services.AddScoped<IValidateOtpHandler, ValidateOtpHandler>();
builder.Services.AddScoped<IGenerateOtpHandler, GenerateOtpHandler>();

// Registrar o Validator e outros serviços
builder.Services.AddScoped<Application.Validators.ClientValidator>();

// Adicionar o Swagger e outros serviços de configuração
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MongoDB Guid Fix
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

// MongoDB Client
builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));

// Health Checks (Readiness + Liveness)
builder.Services.AddHealthChecks()
    .AddMongoDb(
        builder.Configuration.GetConnectionString("MongoDb")!,
        name: "mongodb",
        tags: new[] { "ready" });

var app = builder.Build();

// Usar Swagger para documentação da API
app.UseSwagger();
app.UseSwaggerUI();

// Configurar middleware e roteamento
app.UseHttpsRedirection();
app.UseAuthorization();

// Mapear controladores
app.MapControllers();

app.MapHealthChecks("/health/liveness", new HealthCheckOptions
{
    Predicate = _ => false
});

app.MapHealthChecks("/health/readiness", new HealthCheckOptions
{
    Predicate = r => r.Tags.Contains("ready"),
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

// Rodar a aplicação
app.Run();