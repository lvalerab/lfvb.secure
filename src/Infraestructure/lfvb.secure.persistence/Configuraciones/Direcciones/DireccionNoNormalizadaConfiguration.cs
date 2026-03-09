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
    public class DireccionNoNormalizadaConfiguration
    {
        public DireccionNoNormalizadaConfiguration(EntityTypeBuilder<DireccionNoNormalizadaEntity> builder)
        {
            builder
                .ToTable("dinn_direccion_no_normalizada")
                .HasKey(x => x.Id);


            builder.Property(x => x.Id).HasColumnName("ID_DIRE").IsRequired();
            builder.Property(x => x.IdCalle).HasColumnName("ID_CALLE");
            builder.Property(x => x.IdEntidadTerritorial).HasColumnName("ID_ENTE");
            builder.Property(x => x.Linea1).HasColumnName("LINEA_DIRE_1").HasMaxLength(120);
            builder.Property(x => x.Linea2).HasColumnName("LINEA_DIRE_2").HasMaxLength(120);
            builder.Property(x => x.Linea3).HasColumnName("LINEA_DIRE_3").HasMaxLength(120);

            builder.HasOne(x => x.Direccion)
                .WithOne(d => d.DireccionNoNormalizada)
                .HasForeignKey<DireccionNoNormalizadaEntity>(x => x.Id);

            builder.HasOne(x => x.Callejero)
                .WithMany(c => c.DireccionNoNormalizadas)
                .HasForeignKey(x => x.IdCalle);

            builder
                .HasOne(x => x.EntidadTerritorial)
                .WithMany(et => et.DireccionesNoNormalizadas)
                .HasForeignKey(x => x.IdEntidadTerritorial);
        }
    }
}
