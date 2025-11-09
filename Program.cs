using controleDeFuncionarios.Models;
using controleDeFuncionarios.Dao;
using controleDeFuncionarios.Rotas;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();


app.UseCors("AllowAll");

InicializarBanco.PopularBancoDeDados(app.Services);

app.MapGet("/", () => "Hello World!");
app.MapGetRoutes();
app.MapPostRoutes();
app.MapDeleteRoutes();

app.Run();
