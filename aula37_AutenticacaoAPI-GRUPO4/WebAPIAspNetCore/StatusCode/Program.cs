using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StatusCode;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// -- Parte I

var Chave = Encoding.ASCII.GetBytes(Ambiente.Chave);

// Adiciono os serviços de Autenticação e Autorização utilizando Jwt Bearer
// e configurando Autenticação por padrão como Jwt B e os parametros de validação
// do Token.
builder.Services
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Chave),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// --


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// -- Parte II  

// Adiciono o suporte a Autenticação e Autorização utilizando Jwt Bearer.
app.UseAuthentication();
app.UseAuthorization();

// --
app.UseHttpsRedirection();


app.MapControllers();

app.Run();
