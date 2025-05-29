using Application.Handlers;
using Application.Interfaces;
using Infra.Interfaces;
using Infra.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

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

var app = builder.Build();

// Usar Swagger para documentação da API
app.UseSwagger();
app.UseSwaggerUI();

// Configurar middleware e roteamento
app.UseHttpsRedirection();
app.UseAuthorization();

// Mapear controladores
app.MapControllers();

// Rodar a aplicação
app.Run();