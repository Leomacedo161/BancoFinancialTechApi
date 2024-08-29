using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation.AspNetCore;
using FullTechApiDesafio.Data;
using FullTechApiDesafio.Interface;
using FullTechApiDesafio.Services;
using FullTechApiDesafio.Validations;


var builder = WebApplication.CreateBuilder(args);


// Adiciona serviços de controle (Controllers)
builder.Services.AddControllers();

//Adiciona serviços
builder.Services.AddScoped<IContaService, ContaService>();
builder.Services.AddScoped<ITransferenciaService, TransferenciaService>();
builder.Services.AddScoped<IExtratoService, ExtratoService>();

// Configura Entity Framework com PostgreSQL
builder.Services.AddDbContext<BancoContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("BancoDB")));

// Adiciona serviço de cache em memória
builder.Services.AddMemoryCache();

// Configura autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "teste", 
            ValidAudience = "teste", 
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("segredo")) 
        };
    });

// Configura Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona FluentValidation para validação de modelos
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ContaValidator>());

// Configura HttpClient para chamadas externas
builder.Services.AddHttpClient();

// Registra serviços customizados da aplicação
builder.Services.AddScoped<TransferenciaService>();
builder.Services.AddScoped<ContaService>();
builder.Services.AddScoped<ExtratoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Adiciona autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

// Mapeia os controladores
app.MapControllers();

app.Run();
