using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Aplicacion;
using lfvb.secure.domain.Entities.Credencial;
using lfvb.secure.domain.Entities.GrupoUsuarioAplicacion;
using lfvb.secure.domain.Entities.PasswordCredencial;
using lfvb.secure.domain.Entities.RelacionUsuarioGrupoUsuarioAplicacion;
using lfvb.secure.domain.Entities.TipoCredencial;
using lfvb.secure.domain.Entities.TokenCredencial;
using lfvb.secure.domain.Entities.Usuario;
using lfvb.secure.persistence.Configuraciones;
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
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EntityConfiguration(modelBuilder);
        }
        
    }
}
