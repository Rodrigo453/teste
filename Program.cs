using AlibiPerfeito_CRUD.Data;
using AlibiPerfeito_CRUD.Routes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Configurar o Kestrel para usar a porta 5002
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5001); // Define a porta para a aplicação
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BancoContext>();

// Registrar os serviços de autenticação e autorização
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "http://service-keycloak.keycloak.svc.cluster.local:6000/realms/apps"; 
        //options.Authority = "http://service-keycloak.keycloak.svc.cluster.local:8080/realms/apps"; 
        options.Audience = "API-Gateway";  // Este é o client-id que você configurou no Keycloak
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddAuthorization(); // Registro dos serviços de autorização

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.CategoriaRoutes();

app.DesculpaRoutes();

app.UseHttpsRedirection();
app.Run();