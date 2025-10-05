using lfvb.secure.domain.Entities.Usuario;
using lfvb.secure.persistence.Conversions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones
{
    /// <summary>
    /// Configuracion de la entidad usuario
    /// </summary>
    public class UsuarioConfiguration
    {
        public UsuarioConfiguration(EntityTypeBuilder<UsuarioEntity> entityBuilder) {
            entityBuilder
                  .ToTable("usua_usuarios")
                  .HasKey(x=>x.Id);
            entityBuilder.HasIndex(x => x.Usuario).IsUnique();
            entityBuilder.Property(x => x.Id).IsRequired().HasColumnName("ID_USUA").HasConversion(v=>GuidConversion.toString(v),v=> GuidConversion.toGuid(v));
            entityBuilder.Property(x => x.Nombre).IsRequired().HasColumnName("NOMBRE_USUA");
            entityBuilder.Property(x => x.Apellido1).HasColumnName("APELLIDO1_USUA");
            entityBuilder.Property(x => x.Apellido2).HasColumnName("APELLIDO2_USUA");
            entityBuilder.Property(x => x.Usuario).IsRequired().HasColumnName("USER_USUA").HasConversion(v=>v.ToLower(),v=>v.ToLower());
            entityBuilder.Property(x => x.Email).IsRequired().HasColumnName("EMAIL_USUA").HasConversion(v=>v.ToLower(),v=>v.ToLower());

            //Relaciones
            entityBuilder.HasMany(x => x.Credenciales)
                         .WithOne(x=>x.Usuario)
                         .HasForeignKey(x=>x.IdUsuario);

            entityBuilder.HasMany(x => x.RelacionGrupos)
                         .WithOne(x => x.Usuario)
                         .HasForeignKey(x => x.IdUsuario);

            entityBuilder.HasMany(x => x.PermisosPasos)
                         .WithOne(x => x.Usuario)
                         .HasForeignKey(x => x.IdUsuario);

            entityBuilder.HasMany(x => x.Tramitadores)
                         .WithOne(x => x.UsuarioTramitador)
                         .HasForeignKey(x => x.IdUsuarioTramitador);
        }        
    }
}
