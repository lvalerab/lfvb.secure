using lfvb.secure.api;
using lfvb.secure.aplication;
using lfvb.secure.aplication.Database.Usuario.Commands.CreateUsuario;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.common;
using lfvb.secure.external;
using lfvb.secure.persistence;
using Microsoft.EntityFrameworkCore;
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
        Description = "Esta es la api que proporciona el acceso a la seguridad de usuarios, as� como los m�todos de validaci�n",
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

#region "Configuracion de la inyecci�n de dependencias"

builder.Services.AddWebApi()
        .AddCommon(builder.Configuration)
        .AddPersistence(builder.Configuration)        
        .AddExternal(builder.Configuration)
        .AddAplication(builder.Configuration);
#endregion





var app = builder.Build();

#region "Configuraci�n de la aplicacion"
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

app.MapPost("/addUsuario", async (ICreateUsuarioCommand cm) =>
{
    List<CreateUsuarioModel> lista = new();
    CreateUsuarioModel modelo = new CreateUsuarioModel
    {
        Nombre="Luis Fernando",
        Apellido1="Valera",
        Apellido2="Bernal",
        Usuario="lvalerab2",
        Password="123456"
    };

    modelo = await cm.Execute(modelo);
    lista.Add(modelo);

    
    modelo = new CreateUsuarioModel
    {
        Nombre = "Luis Fernando",
        Apellido1 = "Valera",
        Apellido2 = "Bernal",
        Usuario = "lvalerab3",
        Token = Guid.NewGuid().ToString()
    };

    modelo = await cm.Execute(modelo);
    lista.Add(modelo);

    modelo = new CreateUsuarioModel
    {
        Nombre = "Luis Fernando",
        Apellido1 = "Valera",
        Apellido2 = "Bernal",
        Usuario = "lvalerab4",
        Password="HolaDonPepito",
        Token = Guid.NewGuid().ToString()
    };

    modelo = await cm.Execute(modelo);
    lista.Add(modelo);

    return modelo;
});

app.MapGet("/usuarios", async (IDataBaseService db) =>
{
    var usuarios= await db.Usuarios.ToListAsync();

    return usuarios;
    
});

#endregion

app.Run();
