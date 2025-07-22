using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using API.Handlers;
using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using DA.Contexto;
using Flujo;
using Reglas;
using Servicios;

var builder = WebApplication.CreateBuilder(args);

// --- Authentication & Authorization ---
builder.Services
    .AddAuthentication("HeaderAuth")
    .AddScheme<AuthenticationSchemeOptions, HeaderAuthenticationHandler>(
        "HeaderAuth", options => { });

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("AdminOnly", p => p.RequireRole("admin"));
    opt.AddPolicy("UserOrAdmin", p => p.RequireRole("user", "admin"));
});

// --- Inyección de dependencias ---
builder.Services.AddScoped<IUsuarioDA, UsuarioDA>();
builder.Services.AddScoped<IFavoritoDA, FavoritoDA>();
builder.Services.AddScoped<IListaVisualizacionDA, ListaVisualizacionDA>();

builder.Services.AddScoped<IUsuarioFlujo, UsuarioFlujo>();
builder.Services.AddScoped<IFavoritoFlujo, FavoritoFlujo>();
builder.Services.AddScoped<IListaVisualizacionFlujo, ListaVisualizacionFlujo>();

builder.Services.AddScoped<IConfiguracion, Configuracion>();
builder.Services.AddScoped<IListadoPeliculasReglas, ListadoPeliculasReglas>();
builder.Services.AddScoped<IListadoGenerosReglas, ListadoGenerosReglas>();

builder.Services.AddHttpClient("ServicioPeliculas");
builder.Services.AddScoped<IPeliculaServicio, PeliculasServicio>();
builder.Services.AddScoped<IGeneroServicio, GenerosServicio>();

builder.Services.AddScoped<ISerieServicio, SeriesServicio>();
builder.Services.AddScoped<IListadoSerieReglas, ListadoSeriesReglas>();

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Usuarios y Favoritos",
        Version = "v1"
    });


    c.AddSecurityDefinition("X-User", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        Name = "X-User",
        In = ParameterLocation.Header,
        Description = "Tu nombre de usuario (cabecera X-User)"
    });
    c.AddSecurityDefinition("X-Role", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        Name = "X-Role",
        In = ParameterLocation.Header,
        Description = "Tu rol ('user' o 'admin')"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "X-User"
                }
            },
            new string[] { }
        },
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "X-Role"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    });
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();









