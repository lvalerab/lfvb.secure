using lfvb.secure.domain.Entities.Aplicacion;
using lfvb.secure.domain.Entities.Credencial;
using lfvb.secure.domain.Entities.Elemento;
using lfvb.secure.domain.Entities.ElementoAplicacion;
using lfvb.secure.domain.Entities.GrupoUsuarioAplicacion;
using lfvb.secure.domain.Entities.PasswordCredencial;
using lfvb.secure.domain.Entities.Propiedad;
using lfvb.secure.domain.Entities.PropiedadElemento;
using lfvb.secure.domain.Entities.RelacionGrupoUsuarioElementoAplicacionTipoPermisoAplicacion;
using lfvb.secure.domain.Entities.RelacionTipoElementoTipoPermiso;
using lfvb.secure.domain.Entities.RelacionUsuarioGrupoUsuarioAplicacion;
using lfvb.secure.domain.Entities.TipoCredencial;
using lfvb.secure.domain.Entities.TipoElementoAplicacion;
using lfvb.secure.domain.Entities.TipoPermisoElementoAplicacion;
using lfvb.secure.domain.Entities.TipoPropiedad;
using lfvb.secure.domain.Entities.TokenCredencial;
using lfvb.secure.domain.Entities.Usuario;
using lfvb.secure.domain.Entities.ValorPropiedadElemento;
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

        DbSet<ElementoEntity> Elementos { get; set; }
        DbSet<TipoPropiedadEntity> TiposPropiedades { get; set; }
        DbSet<PropiedadEntity> Propiedades { get; set; }
        DbSet<PropiedadElementoEntity> PropiedadesElementos { get; set; }
        DbSet<ValorPropiedadElementoEntity> ValoresPropiedadesElementos { get; set; }

        Task<bool> SaveAsync();
    }
}
