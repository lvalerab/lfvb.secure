using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Aplicacion;
using lfvb.secure.domain.Entities.Circuitos.Accion;
using lfvb.secure.domain.Entities.Circuitos.AccionTipoElemento;
using lfvb.secure.domain.Entities.Circuitos.AccionUsuario;
using lfvb.secure.domain.Entities.Circuitos.BandejaTramite;
using lfvb.secure.domain.Entities.Circuitos.Circuito;
using lfvb.secure.domain.Entities.Circuitos.Estado;
using lfvb.secure.domain.Entities.Circuitos.EstadoElemento;
using lfvb.secure.domain.Entities.Circuitos.EstadoElementoSiguiente;
using lfvb.secure.domain.Entities.Circuitos.GrupoAdministradorCircuito;
using lfvb.secure.domain.Entities.Circuitos.Paso;
using lfvb.secure.domain.Entities.Circuitos.PasoAccion;
using lfvb.secure.domain.Entities.Circuitos.PasoSiguiente;
using lfvb.secure.domain.Entities.Circuitos.PermisoPasoGrupo;
using lfvb.secure.domain.Entities.Circuitos.PermisoPasoUsuario;
using lfvb.secure.domain.Entities.Circuitos.TipoElementoCircuito;
using lfvb.secure.domain.Entities.Circuitos.Tramite;
using lfvb.secure.domain.Entities.Credencial;
using lfvb.secure.domain.Entities.Elemento;
using lfvb.secure.domain.Entities.ElementoAplicacion;
using lfvb.secure.domain.Entities.GrupoUsuarioAplicacion;
using lfvb.secure.domain.Entities.PasswordCredencial;
using lfvb.secure.domain.Entities.Propiedad;
using lfvb.secure.domain.Entities.PropiedadElemento;
using lfvb.secure.domain.Entities.RelacionGrupoUsuarioElementoAplicacionTipoPermisoAplicacion;
using lfvb.secure.domain.Entities.RelacionTipoElementoPropiedad;
using lfvb.secure.domain.Entities.RelacionTipoElementoTipoPermiso;
using lfvb.secure.domain.Entities.RelacionUsuarioGrupoUsuarioAplicacion;
using lfvb.secure.domain.Entities.TipoCredencial;
using lfvb.secure.domain.Entities.TipoElemento;
using lfvb.secure.domain.Entities.TipoElementoAplicacion;
using lfvb.secure.domain.Entities.TipoPermisoElementoAplicacion;
using lfvb.secure.domain.Entities.TipoPropiedad;
using lfvb.secure.domain.Entities.TokenCredencial;
using lfvb.secure.domain.Entities.Usuario;
using lfvb.secure.domain.Entities.ValorPropiedadElemento;
using lfvb.secure.domain.Entities.Views.VWElemento;
using lfvb.secure.persistence.Configuraciones;
using lfvb.secure.persistence.Configuraciones.Circuitos;
using lfvb.secure.persistence.Configuraciones.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.DataBase
{
    public class DataBaseService:DbContext, IDataBaseService
    {
        public DataBaseService(DbContextOptions options):base(options) { 
            
        }

        

        public DbSet<UsuarioEntity> Usuarios { get; set; }
        public DbSet<TipoCredencialEntity> TiposCredenciales { get; set; }
        public DbSet<CredencialEntity> Credenciales { get; set; }
        public DbSet<PasswordCredencialEntity> Passwords { get; set; }
        public DbSet<TokenCredencialEntity> Tokens { get; set; }
        public DbSet<RelacionUsuarioGrupoUsuarioAplicacionEntity> RelacionUsuariosGruposAplicaciones { get; set; }
        public DbSet<GrupoUsuariosAplicacionEntity> Grupos { get; set; }
        public DbSet<AplicacionEntity> Aplicaciones { get; set; }
        public DbSet<ElementoAplicacionEntity> ElementosAplicaciones { get; set; }
        public DbSet<TipoElementoAplicacionEntity> TiposElementosAplicaciones { get; set; }
        public DbSet<RelacionTipoElementoTipoPermisoEntity> RelacionTiposElementosConTiposPermisos { get; set; }
        public DbSet<TipoPermisoElementoAplicacionEntity> TiposPermisosTipoElementosAplicaciones { get; set; }
        public DbSet<RelacionGrupoUsuarioElementoAplicacionTipoPermisoAplicacionEntity> RelacionElementosConTiposPermisosConGruposUsuarios { get; set; }

        //Circuitos 
        public DbSet<AccionEntity> Acciones { get; set; }   
        public DbSet<AccionTipoElementoEntity> AccionesTiposElementos { get; set; } 
        public DbSet<AccionUsuarioEntity> AccionesUsuarios { get; set; }    
        public DbSet<CircuitoEntity> Circuitos { get; set; }    
        public DbSet<EstadoEntity> Estados { get; set; }    
        public DbSet<EstadoElementoEntity> EstadosElementos { get; set; }
        public DbSet<EstadoElementoSiguienteEntity> EstadosElementosSiguientes { get; set; }    
        public DbSet<GrupoAdministradorCircuitoEntity> GruposAdministradoresCircuitos { get; set; } 
        public DbSet<PasoAccionEntity> PasosAcciones { get; set; }  
        public DbSet<PasoEntity> Pasos { get; set; }    
        public DbSet<PermisoPasoGrupoEntity> PermisosPasosGrupos { get; set; }  
        public DbSet<PermisoPasoUsuarioEntity> PermisosPasosUsuarios { get; set; }  
        public DbSet<TipoElementoCircuitoEntity> TiposElementosCircuitos { get; set; }  
        public DbSet<TramiteEntity> Tramites { get; set; }  
        public DbSet<PasoSiguienteEntity> PasosSiguientes { get; set; }
        public DbSet<BandejaTramiteEntity> BandejasTramites { get; set; }


        #region "Gestion de propiedades de los elementos"
        public DbSet<ElementoEntity> Elementos { get; set; }
        public DbSet<TipoPropiedadEntity> TiposPropiedades { get; set; }
        public DbSet<PropiedadEntity> Propiedades { get; set; }
        public DbSet<PropiedadElementoEntity> PropiedadesElementos { get; set; }
        public DbSet<ValorPropiedadElementoEntity> ValoresPropiedadesElementos { get; set; }
        public DbSet<TipoElementoEntity> TiposElementos { get; set; }
        public DbSet<RelacionTipoElementoPropiedadEntity> RelacionesTiposElementosPropiedades { get; set; }
        #endregion


        #region "Elementos de vistas"
        public DbSet<VWElementoEntity> VistaElementos { get; set; }
        #endregion

        public async Task<bool> SaveAsync()
        {
            return await SaveChangesAsync() > 0;
        }

        //Para cargar la configuracion de los modelos

        private void EntityConfiguration(ModelBuilder modelBuilder)
        {
            new UsuarioConfiguration(modelBuilder.Entity<UsuarioEntity>());
            new TipoCredencialConfiguration(modelBuilder.Entity<TipoCredencialEntity>());
            new CredencialConfiguration(modelBuilder.Entity<CredencialEntity>());
            new PasswordCredencialConfiguration(modelBuilder.Entity<PasswordCredencialEntity>());
            new TokenCredencialConfiguration(modelBuilder.Entity<TokenCredencialEntity>());
            new RelacionUsuarioGrupoUsuarioAplicacionConfiguration(modelBuilder.Entity<RelacionUsuarioGrupoUsuarioAplicacionEntity>());
            new GrupoUsuariosAplicacionConfiguration(modelBuilder.Entity<GrupoUsuariosAplicacionEntity>());
            new AplicacionConfiguration(modelBuilder.Entity<AplicacionEntity>());
            new ElementoAplicacionConfiguration(modelBuilder.Entity<ElementoAplicacionEntity>());
            new TipoElementoAplicacionConfiguration(modelBuilder.Entity<TipoElementoAplicacionEntity>());
            new RelacionTipoElementoTipoPermisoConfiguration(modelBuilder.Entity<RelacionTipoElementoTipoPermisoEntity>());
            new TipoPermisoElementoAplicacionConfiguration(modelBuilder.Entity<TipoPermisoElementoAplicacionEntity>());
            new RelacionGrupoUsuarioElementoAplicacionTipoPermisoAplicacionConfiguration(modelBuilder.Entity<RelacionGrupoUsuarioElementoAplicacionTipoPermisoAplicacionEntity>());

            new ElementoConfiguration(modelBuilder.Entity<ElementoEntity>());
            new TipoPropiedadConfiguracion(modelBuilder.Entity<TipoPropiedadEntity>());
            new PropiedadConfiguration(modelBuilder.Entity<PropiedadEntity>());
            new PropiedadElementoConfiguration(modelBuilder.Entity<PropiedadElementoEntity>());
            new ValorPropiedadElementoConfiguration(modelBuilder.Entity<ValorPropiedadElementoEntity>());
            new TipoElementoConfiguration(modelBuilder.Entity<TipoElementoEntity>());
            new RelacionTipoElementoPropiedadConfiguracion(modelBuilder.Entity<RelacionTipoElementoPropiedadEntity>());

            //Modulo de circuitos
            new AccionConfiguration(modelBuilder.Entity<AccionEntity>());   
            new AccionTipoElementoConfiguration(modelBuilder.Entity<AccionTipoElementoEntity>());   
            new AccionUsuarioConfiguration(modelBuilder.Entity<AccionUsuarioEntity>()); 
            new CircuitoConfiguration(modelBuilder.Entity<CircuitoEntity>());
            new EstadoConfiguration(modelBuilder.Entity<EstadoEntity>());
            new EstadoElementoConfiguration(modelBuilder.Entity<EstadoElementoEntity>());
            new EstadoElementoSiguienteConfiguration(modelBuilder.Entity<EstadoElementoSiguienteEntity>());
            new GrupoAdministradorCircuitoConfiguration(modelBuilder.Entity<GrupoAdministradorCircuitoEntity>());   
            new PasoAccionConfiguration(modelBuilder.Entity<PasoAccionEntity>());
            new PasoConfiguration(modelBuilder.Entity<PasoEntity>());
            new PermisoPasoGrupoConfiguration(modelBuilder.Entity<PermisoPasoGrupoEntity>());   
            new PermisoPasoUsuarioConfiguration(modelBuilder.Entity<PermisoPasoUsuarioEntity>());   
            new TipoElementoCircuitoConfiguration(modelBuilder.Entity<TipoElementoCircuitoEntity>());   
            new TramiteConfiguration(modelBuilder.Entity<TramiteEntity>());
            new PasoSiguienteConfiguration(modelBuilder.Entity<PasoSiguienteEntity>()); 
            new BandejaTramiteConfiguration(modelBuilder.Entity<BandejaTramiteEntity>());   


            new VWElementoConfiguration(modelBuilder.Entity<VWElementoEntity>());
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EntityConfiguration(modelBuilder);
        }
        
    }
}
