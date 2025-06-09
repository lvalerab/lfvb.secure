using lfvb.secure.domain.Entities.Propiedad;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones
{
    public class PropiedadConfiguration
    {
        public PropiedadConfiguration(EntityTypeBuilder<PropiedadEntity> builder)
        {
            builder
                .ToTable("prop_propiedad")
                .HasKey(x => x.Codigo);

            builder.Property(x => x.Codigo).HasColumnName("COD_PROP").IsRequired().HasMaxLength(10);
            builder.Property(x => x.CodigoPadre).HasColumnName("COD_PROP_PADRE").HasMaxLength(10);
            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_PROP").IsRequired().HasMaxLength(255);
            builder.Property(x => x.CodTipoPropiedad).HasColumnName("COD_TPPR").IsRequired().HasMaxLength(255);

            //Relacion 1 a muchos
            builder.HasMany(x => x.PropiedadesHijas)
                .WithOne(x => x.PropiedadPadre)
                .HasForeignKey(x => x.CodigoPadre);

            builder.HasMany(x => x.PropiedadesElementos)
                .WithOne(x => x.Propiedad)
                .HasForeignKey(x => x.CodPropiedad);

            builder.HasOne(x => x.TipoPropiedad)
                .WithMany(x => x.Propiedades)
                .HasForeignKey(x => x.CodTipoPropiedad);

            builder
                .HasMany(x=>x.RelacionTiposElementos)
                .WithOne(x=>x.Propiedad)
                .HasForeignKey(x=>x.CodigoPropiedad);


        }
    }
}
