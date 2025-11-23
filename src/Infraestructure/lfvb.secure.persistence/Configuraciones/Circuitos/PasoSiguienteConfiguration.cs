using lfvb.secure.domain.Entities.Circuitos.PasoSiguiente;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Circuitos
{
    public class PasoSiguienteConfiguration
    {
        public PasoSiguienteConfiguration(EntityTypeBuilder<PasoSiguienteEntity> builder)
        {
            builder
                .ToTable("PSSG_PASO_SIGUIENTE")
                .HasKey(x => new { x.IdPaso, x.IdPasoSiguiente });
            builder.Property(x => x.IdPaso).HasColumnName("ID_PASO").IsRequired();
            builder.Property(x => x.IdPasoSiguiente).HasColumnName("ID_PASO_SIG").IsRequired();
            builder.HasOne(x => x.Paso).WithMany(p => p.PasosSiguientes).HasForeignKey(x => x.IdPaso);
            builder.HasOne(x => x.PasoSiguiente).WithMany(p => p.PasosPrevios).HasForeignKey(x => x.IdPasoSiguiente);
        }
    }
}
