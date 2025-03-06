using System.Reflection;
using api_filmes_senai.Context;
using api_filmes_senai.Interfaces;
using api_filmes_senai.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o contexto do banco de dados (exemplo com SQL Server)
builder.Services.AddDbContext<Filmes_Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Adicionar o reposit�rio e a interface ao container de inje��o de depend�ncia
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>(); 
builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

//Adicionar o servi�o de Controlllers
builder.Services.AddControllers();

//Adicionar servi�o de JWT Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new
    TokenValidationParameters
    {
        //valida quem est� solicitando
        ValidateIssuer = true,
        //valida quem est� recebendo
        ValidateAudience = true,
        //define se o tempo de expira��o ser� validado
        ValidateLifetime = true,
        //forma de criptografia e valida a chave de autentica��o
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev")),

        //valida o tempo de expira��o do token
        ClockSkew = TimeSpan.FromMinutes(5),

        //valida de onde est� vindo
        ValidIssuer = "api_filmes_senai",

        ValidAudience = "api_filmes_senai"
    };

});

//Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API de Filmes",
        Description = "Aplica��o para gerenciamento de filmes e G�neros",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Sillas Endrigo",
            Url = new Uri("https://github.com/Sillocazord")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));


});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });

    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

//Adicionar o mapeamento dos Controllers
app.MapControllers();
//Adicionar a autoriza��o (se voce tem permiss�o
app.UseAuthentication();
//Adicionar a autentica��o (se voce � voce mesmo
app.UseAuthorization();

app.Run();
