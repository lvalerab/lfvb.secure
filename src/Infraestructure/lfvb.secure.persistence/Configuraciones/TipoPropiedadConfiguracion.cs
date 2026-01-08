using lfvb.secure.domain.Entities.TipoPropiedad;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones
{
    public class TipoPropiedadConfiguracion
    {
        public TipoPropiedadConfiguracion(EntityTypeBuilder<TipoPropiedadEntity> builder)
        {
            builder
                .ToTable("tppr_tipo_propiedad")
                .HasKey(x => x.Codigo);

            builder.Property(x => x.Codigo).HasColumnName("COD_TPPR").IsRequired();
            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_TPPR").IsRequired().HasMaxLength(255);
            builder.Property(x => x.Multiple).HasColumnName("MULTI").IsRequired().HasMaxLength(1).HasDefaultValue("N");
            builder.Property(x => x.Historico).HasColumnName("HISTO").IsRequired().HasMaxLength(1).HasDefaultValue("N");
            builder.Property(x => x.Intervalo).HasColumnName("INTERVALO").IsRequired().HasMaxLength(1).HasDefaultValue("N");
            builder.Property(x => x.Tipo).HasColumnName("TIPO_VALOR").IsRequired();
            builder.Property(x => x.ListaValores).HasColumnName("VALORES_SQL").IsRequired().HasMaxLength(1).HasDefaultValue("N");

            builder.HasMany(x => x.Propiedades)
                .WithOne(x => x.TipoPropiedad)
                .HasForeignKey(x => x.CodTipoPropiedad);

        }
    }
}
