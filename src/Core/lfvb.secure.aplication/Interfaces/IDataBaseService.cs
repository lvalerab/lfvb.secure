using lfvb.secure.domain.Entities.Credencial;
using lfvb.secure.domain.Entities.TipoCredencial;
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

        Task<bool> SaveAsync();
    }
}
