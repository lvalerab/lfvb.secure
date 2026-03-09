using lfvb.secure.domain.Entities.Direcciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Direcciones
{
    public class DireccionNormalizadaConfiguration
    {
        public DireccionNormalizadaConfiguration(EntityTypeBuilder<DireccionNormalizadaEntity> builder)
        {
            builder
                .ToTable("dirn_direccion_normalizada")
                .HasKey(x => x.Id); 

            builder.Property(x => x.Id).HasColumnName("ID_DIRE").IsRequired();
            builder.Property(x => x.IdCalle).HasColumnName("ID_CALLE").IsRequired();
            builder.Property(x => x.Edificio).HasColumnName("EDIF_DIRN").HasMaxLength(255);
            builder.Property(x => x.Numero).HasColumnName("NUMERO_DIRN").HasMaxLength(50);
            builder.Property(x => x.Puerta).HasColumnName("PUERTA_DIRN").HasMaxLength(50);
            builder.Property(x => x.Piso).HasColumnName("PISO_DIRN").HasMaxLength(50);
            builder.Property(x => x.Escalera).HasColumnName("ESCA_DIRN").HasMaxLength(50);
            builder.Property(x => x.Bloque).HasColumnName("BLOQ_DIRN").HasMaxLength(50);
            builder.Property(x => x.Ampliacion).HasColumnName("AMPLIACION_DIRN").HasMaxLength(255);

            builder.HasOne(x => x.Direccion)
                .WithOne(d=>d.DireccionNormalizada)
                .HasForeignKey<DireccionNormalizadaEntity>(x => x.Id);

            builder.HasOne(x => x.Callejero)
                .WithMany(c=>c.DireccionNormalizadas)
                .HasForeignKey(x => x.IdCalle);
        }
    }
}
