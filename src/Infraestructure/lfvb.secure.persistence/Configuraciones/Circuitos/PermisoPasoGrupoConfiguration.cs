using lfvb.secure.domain.Entities.Circuitos.PermisoPasoGrupo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Circuitos
{
    public class PermisoPasoGrupoConfiguration
    {
        public PermisoPasoGrupoConfiguration(EntityTypeBuilder<PermisoPasoGrupoEntity> builder)
        {
            builder
                 .ToTable("PSGU_PASO_GUAP")
                 .HasKey(x => new { x.IdPaso, x.IdGrupoUsuario });

            builder.Property(x => x.IdPaso).HasColumnName("ID_PASO").IsRequired();
            builder.Property(x => x.IdGrupoUsuario).HasColumnName("ID_GUAP").IsRequired();

            builder.HasOne(x => x.Paso).WithMany(p => p.PermisosGrupos).HasForeignKey(x => x.IdPaso);
            builder.HasOne(x => x.GrupoUsuario).WithMany(g => g.PermisoPasos).HasForeignKey(x => x.IdGrupoUsuario);
        }
    }
}
