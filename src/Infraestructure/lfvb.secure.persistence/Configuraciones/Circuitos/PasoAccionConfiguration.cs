using lfvb.secure.domain.Entities.Circuitos.PasoAccion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Circuitos
{
    public class PasoAccionConfiguration
    {

        public PasoAccionConfiguration(EntityTypeBuilder<PasoAccionEntity> builder) 
        {

            builder
                .ToTable("PSAC_PASO_ACCION")
                .HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID_PSAC").HasComputedColumnSql().IsRequired();
            builder.Property(x => x.IdPaso).HasColumnName("ID_PASO").IsRequired();
            builder.Property(x => x.TipoEjecucion).HasColumnName("TIPO_EJEC");
            builder.Property(x => x.Orden).HasColumnName("ORDEN_EJECUCION").HasDefaultValue(0);
            builder.Property(x => x.IdAccion).HasColumnName("ID_ACCI");
            builder.Property(x => x.IdCircuitoError).HasColumnName("ID_CIRC_ERROR");

            builder.HasOne(x => x.Paso).WithMany(p => p.Acciones).HasForeignKey(x => x.IdPaso);
            builder.HasOne(x => x.Accion).WithMany(a => a.Pasos).HasForeignKey(x => x.IdAccion);
            builder.HasOne(x => x.CircuitoError).WithMany(c => c.PasosErrores).HasForeignKey(x => x.IdCircuitoError);
            
        }
    }
}
