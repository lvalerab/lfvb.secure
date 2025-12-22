using lfvb.secure.aplication.Database.Circuitos.Acciones.Queries;
using lfvb.secure.aplication.Database.Circuitos.AccionesPasos.Queries;
using lfvb.secure.aplication.Database.Circuitos.BandejaTramites.Queries;
using lfvb.secure.aplication.Database.Circuitos.Circuitos.Commands;
using lfvb.secure.aplication.Database.Circuitos.Circuitos.Commands.Pasos;
using lfvb.secure.aplication.Database.Circuitos.Circuitos.Queries;
using lfvb.secure.aplication.Database.Circuitos.Circuitos.Queries.Pasos;
using lfvb.secure.aplication.Database.Circuitos.Estados;
using lfvb.secure.aplication.Database.Circuitos.Tramites.Commands;
using lfvb.secure.aplication.Database.Circuitos.Tramites.Queries.GetAllTramites;
using lfvb.secure.aplication.Database.Circuitos.Tramites.Queries.GetTramite;
using Microsoft.Extensions.DependencyInjection;


namespace lfvb.secure.aplication.Database.Circuitos
{
    public  class CircuitosDependencyInjection
    {
        public static IServiceCollection AddCommands(IServiceCollection services)
        {
            services.AddTransient<IAltaTramiteCommand, AltaTramiteCommand>();
            services.AddTransient<IModificarTramiteCommand, ModificarTramiteCommand>(); 

            services.AddTransient<IAltaCircuitoCommand, AltaCircuitoCommand>();
            services.AddTransient<IModificacionCircuitoCommand, ModificarCircuitoCommand>();   


            services.AddTransient<IAltaPasoCircuitoCommand, AltaPasoCircuitoCommand>(); 
            services.AddTransient<IModificarPasoCircuitoCommand, ModificarPasoCircuitoCommand>();
            services.AddTransient<IEliminarPasoCircuitoCommand, EliminarPasoCircuitoCommand>(); 

            services.AddTransient<IAltaPasoSiguienteCircuitoCommand, AltaPasoSiguienteCircuitoCommand>();
            services.AddTransient<IEliminarPasosSiguienteCircuitoCommand, EliminarPasosSiguienteCircuitoCommand>();   

            return services;
        }

        public static IServiceCollection AddQuerys(IServiceCollection services)
        {

            services.AddTransient<IGetAllTramitesQuery, GetAllTramitesQuery>();
            services.AddTransient<IGetTramiteQuery, GetTramiteQuery>();

            services.AddTransient<IGetCircuitosQuery, GetCircuitosQuery>();
            services.AddTransient<IGetCircuitoQuery, GetCircuitoQuery>();

            services.AddTransient<IGetPasosCircuitoQuery, GetPasosCircuitoQuery>();
            services.AddTransient<IGetPasoCircuitoQuery, GetPasoCircuitoQuery>();

            services.AddTransient<IEstadosElementosQuery, EstadosElementosQuery>(); 

            services.AddTransient<IGetAllAccionesQuery, GetAllAccionesQuery>();

            services.AddTransient<IGetAccionesPasoQuery, GetAccionesPasoQuery>();

            services.AddTransient<IListaBandejasSistemaQuery, ListaBandejasSistemaQuery>();


            return services;
        }   
    }
}
