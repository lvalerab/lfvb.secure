using lfvb.secure.domain.Entities.TipoElementoAplicacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones
{
    public class TipoElementoAplicacionConfiguration
    {
        public TipoElementoAplicacionConfiguration(EntityTypeBuilder<TipoElementoAplicacionEntity> builder)
        {
            builder
                .ToTable("TEAP_TIPO_ELEMENTOS_APLICACION")
                .HasKey(x => x.Codigo);

            builder.Property(x => x.Codigo).HasColumnName("COD_TEAP").IsRequired();
            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_TEAP").IsRequired();

            builder.HasMany(x => x.Elementos)
                .WithOne(x => x.TipoElementoAplicacion)
                .HasForeignKey(x => x.CodigoTipoElemento);

            builder.HasMany(x => x.RelacionTiposPermisos)
                .WithOne(x => x.TipoElemento)
                .HasForeignKey(x => x.CodigoTipoElemento);

        }
    }
}
