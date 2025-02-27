using api_filmes_senai.Context;
using api_filmes_senai.Interfaces;
using api_filmes_senai.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o contexto do banco de dados (exemplo com SQL Server)
builder.Services.AddDbContext<Filmes_Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Adicionar o repositório e a interface ao container de injeção de dependência
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>(); 
builder.Services.AddScoped<IFilmeRepositoty, FilmeRepository>();

//Adicionar o serviço de Controlllers
builder.Services.AddControllers();

var app = builder.Build();

//Adicionar o mapeamento dos Controllers
app.MapControllers();

app.Run();
