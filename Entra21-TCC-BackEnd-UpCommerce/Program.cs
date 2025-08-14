using Entra21_TCC_BackEnd_UpCommerce.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// =====================================
// Configurações de Serviços (ANTES do Build)
// =====================================

// 1️⃣ CORS - permitir Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // endereço do Angular
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// 2️⃣ Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3️⃣ Banco de Dados MySQL
var connectionString = builder.Configuration.GetConnectionString("MySql");
builder.Services.AddDbContext<AppDb>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// 4️⃣ Autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// 5️⃣ Autorização
builder.Services.AddAuthorization();

// =====================================
// Construir aplicação
// =====================================
var app = builder.Build();

// =====================================
// Pipeline
// =====================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 6️⃣ Usar CORS antes de Authentication/Authorization
app.UseCors("AllowAngularDev");

// Necessário para usar [Authorize]
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
