using lfvb.secure.api;
using lfvb.secure.aplication;
using lfvb.secure.aplication.Database.Usuario.Commands.CreateUsuario;
using lfvb.secure.aplication.Database.Usuario.Queries.GetAllUsuarios;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.common;
using lfvb.secure.external;
using lfvb.secure.persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Cryptography.Xml;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region "Configuracion de swagger"

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options=>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Api de seguridad y acceso usuarios",
        Description = "Esta es la api que proporciona el acceso a la seguridad de usuarios, así como los métodos de validación",
        Contact = new OpenApiContact
        {
            Name = "Luis Fernando Valera Bernal",
            Url = new Uri("https://www.lfvb.es/contact/lfvb")
        }
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description="JWT Authorization",
        Name="Authorization",
        In=ParameterLocation.Header,
        Type=SecuritySchemeType.ApiKey,
        Scheme="Bearer"
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{ }
        }
    });
    try { 
        // using System.Reflection;
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    } catch (Exception ex)
    {
        Console.WriteLine("Error al documentar SWAGGER, causa: " + ex.Message);
    }
});

#endregion

#region "Configuracion de la inyección de dependencias"

builder.Services
        .AddJwtSecurity(builder.Configuration) //Para insertar la configuracion de la securizacion por JWT
        .AddWebApi() //Para los servidios de web api
        .AddCommon(builder.Configuration) //Para incluir la capa de Common
        .AddPersistence(builder.Configuration) //PAra incluir la capa de persistencia (Infraestructura)
        .AddExternal(builder.Configuration) //Para incluir la capa de datos externos (Infraestrucutra)
        .AddAplication(builder.Configuration); //PAra incluir la capa de aplicacion (Core)
#endregion





var app = builder.Build();

#region "Configuración de la aplicacion"
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();
