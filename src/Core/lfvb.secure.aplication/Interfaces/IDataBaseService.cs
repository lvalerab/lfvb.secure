using lfvb.secure.domain.Entities.Aplicacion;
using lfvb.secure.domain.Entities.Credencial;
using lfvb.secure.domain.Entities.GrupoUsuarioAplicacion;
using lfvb.secure.domain.Entities.PasswordCredencial;
using lfvb.secure.domain.Entities.RelacionUsuarioGrupoUsuarioAplicacion;
using lfvb.secure.domain.Entities.TipoCredencial;
using lfvb.secure.domain.Entities.TokenCredencial;
using lfvb.secure.domain.Entities.Usuario;
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

        Task<bool> SaveAsync();
    }
}
