using lfvb.secure.domain.Entities.Calendario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Calendario
{
    public class TipoEntradaCalendarioConfiguration
    {
        public TipoEntradaCalendarioConfiguration(EntityTypeBuilder<TipoEntradaCalendarioEntity> builder)
        {
            builder
                .ToTable("tenc_tipo_entrada_calendario")
                .HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID_TENC").IsRequired();
            builder.Property(x => x.Codigo).HasColumnName("COD_TENC").IsRequired().HasMaxLength(10);
            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_TENC").IsRequired();

            builder.HasMany(x => x.EntradasCalendario)
                .WithOne(x => x.TipoEntrada)
                .HasForeignKey(x => x.IdTipoEntradaCalendario);
        }
    }
}
