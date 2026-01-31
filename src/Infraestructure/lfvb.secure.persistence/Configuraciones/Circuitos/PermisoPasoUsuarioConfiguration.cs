using lfvb.secure.domain.Entities.Circuitos.PermisoPasoUsuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Circuitos
{
    public class PermisoPasoUsuarioConfiguration
    {
        public PermisoPasoUsuarioConfiguration(EntityTypeBuilder<PermisoPasoUsuarioEntity> builder)
        {
            builder
                .ToTable("PSUS_PASO_USUA")
                .HasKey(x => new { x.IdPaso, x.IdUsuario });

            builder.Property(x => x.IdPaso).HasColumnName("ID_PASO").IsRequired();
            builder.Property(x => x.IdUsuario).HasColumnName("ID_USUA").IsRequired();

            builder.HasOne(x => x.Usuario).WithMany(u => u.PermisosPasos).HasForeignKey(x => x.IdUsuario);
            builder.HasOne(x => x.Paso).WithMany(u => u.PermisoUsuarios).HasForeignKey(x => x.IdPaso);
        }
    }
}
