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
    public class CallejeroConfiguration
    {
        public CallejeroConfiguration(EntityTypeBuilder<CallejeroEntity> builder)
        {
            builder
                .ToTable("calle_callejero")
                .HasKey(x => x.Id); 

            builder.Property(x => x.Id).HasColumnName("ID_CALLE").IsRequired();
            builder.Property(x => x.IdEntidadTerritorial).HasColumnName("ID_ENTE").IsRequired();
            builder.Property(x => x.IdCalleSuperior).HasColumnName("ID_CALLE_SUP");
            builder.Property(x => x.CodigoTipoVia).HasColumnName("COD_TIVI").HasMaxLength(10).IsRequired();
            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_CALLE").HasMaxLength(255).IsRequired();

            builder.HasOne(x => x.EntidadTerritorial)
                .WithMany(et => et.Calles)
                .HasForeignKey(x => x.IdEntidadTerritorial);

            builder.HasOne(x => x.CalleSuperior)
                .WithMany(c=>c.CallesInferiores)
                .HasForeignKey(x => x.IdCalleSuperior);

            builder.HasOne(x => x.TipoVia)
                .WithMany(tv => tv.Vias)
                .HasForeignKey(x => x.CodigoTipoVia);

            builder.HasMany(x => x.DireccionNormalizadas)
                .WithOne(d => d.Callejero)
                .HasForeignKey(d => d.IdCalle);

            builder.HasMany(x => x.DireccionNoNormalizadas)
                .WithOne(d => d.Callejero)
                .HasForeignKey(d => d.IdCalle);
        }   
    }
}
