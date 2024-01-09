using lfvb.secure.api;
using lfvb.secure.aplication;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.common;
using lfvb.secure.external;
using lfvb.secure.persistence;
using lfvb.secure.persistence.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region "Configuracion de swagger"

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options=>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Api de seguridad y acceso usuarios",
        Description = "Esta es la api que proporciona el acceso a la seguridad de usuarios, así como los métodos de validación",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Luis Fernando Valera Bernal",
            Url = new Uri("https://www.lfvb.es/contact/lfvb")
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

builder.Services.AddWebApi()
        .AddCommon(builder.Configuration)
        .AddAplication(builder.Configuration)
        .AddExternal(builder.Configuration)
        .AddPersistence(builder.Configuration);
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

app.UseAuthorization();

app.MapControllers();

#endregion

#region "Minimal api para pruebas"

app.MapPost("/addUsuario", async (IDataBaseService db) =>
{
    var entitity = new lfvb.secure.domain.Entities.Usuario.UsuarioEntity
    {
        Id=Guid.NewGuid(),
        Nombre="Luis Fernando",
        Apellido1="Valera",
        Apellido2="Bernal",
        Usuario="lvalera"
    };

    await db.Usuarios.AddAsync(entitity);

    await db.SaveAsync();

    return entitity;
});

app.MapGet("/usuarios", async (IDataBaseService db) =>
{
    var usuarios= await db.Usuarios.ToListAsync();

    return usuarios;
    
});

#endregion

app.Run();
