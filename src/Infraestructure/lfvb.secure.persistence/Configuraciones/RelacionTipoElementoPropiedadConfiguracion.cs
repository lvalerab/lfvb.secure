using lfvb.secure.domain.Entities.RelacionTipoElementoPropiedad;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones
{
    public class RelacionTipoElementoPropiedadConfiguracion
    {
        public RelacionTipoElementoPropiedadConfiguracion(EntityTypeBuilder<RelacionTipoElementoPropiedadEntity> entityTypeBuilder)
        {
            entityTypeBuilder
                    .ToTable("prti_prop_tiel")
                    .HasKey(x => new { x.CodigoPropiedad,x.CodigoTipoElemento});

            entityTypeBuilder
                .Property(x => x.CodigoPropiedad).HasColumnName("COD_TIEL");
            entityTypeBuilder
                .Property(x => x.CodigoTipoElemento).HasColumnName("COD_PROP");

            entityTypeBuilder
                .HasOne(x => x.Propiedad)
                .WithMany(x => x.RelacionTiposElementos)
                .HasForeignKey(x => x.CodigoPropiedad);

            entityTypeBuilder
                .HasOne(x => x.TipoElemento)
                .WithMany(x => x.RelacionPropiedades)
                .HasForeignKey(x => x.CodigoPropiedad);
        }

    }
}
