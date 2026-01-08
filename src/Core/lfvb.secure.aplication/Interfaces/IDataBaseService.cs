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
using lfvb.secure.domain.Entities.EstadoEsperadoPaso;
using lfvb.secure.domain.Entities.GrupoUnidadOrganizativa;
using lfvb.secure.domain.Entities.GrupoUsuarioAplicacion;
using lfvb.secure.domain.Entities.PasswordCredencial;
using lfvb.secure.domain.Entities.Propiedad;
using lfvb.secure.domain.Entities.PropiedadElemento;
using lfvb.secure.domain.Entities.PropiedadValoresSql;
using lfvb.secure.domain.Entities.RelacionGrupoUsuarioElementoAplicacionTipoPermisoAplicacion;
using lfvb.secure.domain.Entities.RelacionTipoElementoPropiedad;
using lfvb.secure.domain.Entities.RelacionTipoElementoTipoPermiso;
using lfvb.secure.domain.Entities.RelacionUsuarioGrupoUsuarioAplicacion;
using lfvb.secure.domain.Entities.TipoCredencial;
using lfvb.secure.domain.Entities.TipoElemento;
using lfvb.secure.domain.Entities.TipoElementoAplicacion;
using lfvb.secure.domain.Entities.TipoPermisoElementoAplicacion;
using lfvb.secure.domain.Entities.TipoPropiedad;
using lfvb.secure.domain.Entities.TipoUnidadOrganizativa;
using lfvb.secure.domain.Entities.TokenCredencial;
using lfvb.secure.domain.Entities.UnidadOrganizativa;
using lfvb.secure.domain.Entities.UnidadOrganizativaElemento;
using lfvb.secure.domain.Entities.Usuario;
using lfvb.secure.domain.Entities.ValorPropiedadElemento;
using lfvb.secure.domain.Entities.Views.VWElemento;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Interfaces
{
    public interface IDataBaseService
    {
        DbSet<UsuarioEntity> Usuarios { get; set; }
        DbSet<TipoCredencialEntity> TiposCredenciales { get; set; }
        DbSet<CredencialEntity> Credenciales { get; set; }
        DbSet<PasswordCredencialEntity> Passwords { get; set; }
        DbSet<TokenCredencialEntity> Tokens { get; set; }
        DbSet<RelacionUsuarioGrupoUsuarioAplicacionEntity> RelacionUsuariosGruposAplicaciones { get; set; }
        DbSet<GrupoUsuariosAplicacionEntity> Grupos { get; set; }
        DbSet<AplicacionEntity> Aplicaciones { get; set; }
        DbSet<ElementoAplicacionEntity> ElementosAplicaciones { get; set; }
        DbSet<TipoElementoAplicacionEntity> TiposElementosAplicaciones { get; set; }
        DbSet<RelacionTipoElementoTipoPermisoEntity> RelacionTiposElementosConTiposPermisos { get; set; }
        DbSet<TipoPermisoElementoAplicacionEntity> TiposPermisosTipoElementosAplicaciones { get; set; }
        DbSet<RelacionGrupoUsuarioElementoAplicacionTipoPermisoAplicacionEntity> RelacionElementosConTiposPermisosConGruposUsuarios { get; set; }

        #region "modulo de circuitos"   
        DbSet<AccionEntity> Acciones { get; set; }
        DbSet<AccionTipoElementoEntity> AccionesTiposElementos { get; set; }
        DbSet<AccionUsuarioEntity> AccionesUsuarios { get; set; }
        DbSet<CircuitoEntity> Circuitos { get; set; }
        DbSet<EstadoEntity> Estados { get; set; }
        DbSet<EstadoElementoEntity> EstadosElementos { get; set; }
        DbSet<EstadoElementoSiguienteEntity> EstadosElementosSiguientes { get; set; }
        DbSet<GrupoAdministradorCircuitoEntity> GruposAdministradoresCircuitos { get; set; }
        DbSet<PasoAccionEntity> PasosAcciones { get; set; }
        DbSet<PasoEntity> Pasos { get; set; }
        DbSet<PermisoPasoGrupoEntity> PermisosPasosGrupos { get; set; }
        DbSet<PermisoPasoUsuarioEntity> PermisosPasosUsuarios { get; set; }
        DbSet<TipoElementoCircuitoEntity> TiposElementosCircuitos { get; set; }
        DbSet<TramiteEntity> Tramites { get; set; }
        DbSet<PasoSiguienteEntity> PasosSiguientes { get; set; }
        DbSet<BandejaTramiteEntity> BandejasTramites { get; set; }
        DbSet<EstadoElementoSiguienteEntity> EstadoElementoSiguientes { get; set; }
        DbSet<EstadoEsperadoPasoEntity> EstadosEsperadosPasos { get; set; } 
        #endregion

        #region "Modulo de propiedades"
        DbSet<ElementoEntity> Elementos { get; set; }
        DbSet<TipoPropiedadEntity> TiposPropiedades { get; set; }
        DbSet<PropiedadEntity> Propiedades { get; set; }
        DbSet<PropiedadElementoEntity> PropiedadesElementos { get; set; }
        DbSet<ValorPropiedadElementoEntity> ValoresPropiedadesElementos { get; set; }
        DbSet<TipoElementoEntity> TiposElementos { get; set; }
        DbSet<RelacionTipoElementoPropiedadEntity> RelacionesTiposElementosPropiedades { get; set; }
        DbSet<PropiedadValoresSqlEntity> PropiedadesValoresSql { get; set; }
        #endregion

        #region "Unidades organizativas"
        DbSet<TipoUnidadOrganizativaEntity> TiposUnidadesOrganizativas { get; set; }
        DbSet<UnidadOrganizativaEntity> UnidadesOrganizativas { get; set; }
        DbSet<GrupoUnidadOrganizativaEntity> GruposUnidadesOrganizativas { get; set; }
        DbSet<UnidadOrganizativaElementoEntity> UnidadesOrganizativasElementos { get; set; }
        #endregion

        #region "Elementos de vistas"
        DbSet<VWElementoEntity> VistaElementos { get; set; }
        #endregion

        Task<bool> SaveAsync();

        IQueryable<T> FromSql<T>(string sql, params object?[] parametros) where T : class;   
    }
}
