using lfvb.secure.domain.Entities.RelacionUsuarioGrupoUsuarioAplicacion;
using lfvb.secure.domain.Entities.Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones
{
    public class RelacionUsuarioGrupoUsuarioAplicacionConfiguration
    {
        public RelacionUsuarioGrupoUsuarioAplicacionConfiguration(EntityTypeBuilder<RelacionUsuarioGrupoUsuarioAplicacionEntity> builder)
        {
            builder
                .ToTable("usgu_usua_guap")
                .HasKey(x => new { x.IdUsuario, x.IdGrupo });

            builder.Property(x => x.IdUsuario).HasColumnName("ID_USUA").IsRequired();
            builder.Property(x => x.IdGrupo).HasColumnName("ID_GUAP").IsRequired();

            //Relaciones 1 to N
            builder.HasOne(x => x.Usuario)
                   .WithMany(x => x.RelacionGrupos)
                   .HasForeignKey(x => x.IdUsuario);

            builder.HasOne(x => x.Grupo)
                    .WithMany(x => x.RelacionUsuarios)
                    .HasForeignKey(x => x.IdGrupo);
                
        }
    }
}
