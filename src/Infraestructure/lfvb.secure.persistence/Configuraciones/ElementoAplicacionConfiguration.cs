using lfvb.secure.domain.Entities.ElementoAplicacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones
{
    public class ElementoAplicacionConfiguration
    {

        public ElementoAplicacionConfiguration(EntityTypeBuilder<ElementoAplicacionEntity> builder)
        {
            builder
                .ToTable("ELAP_ELEMENTO_APLI")
                .HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID_ELAP").IsRequired();
            builder.Property(x => x.CodigoTipoElemento).HasColumnName("COD_TEAP").IsRequired();
            builder.Property(x => x.IdAplicacion).HasColumnName("ID_APLI").IsRequired();
            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_ELAP").IsRequired();
            builder.Property(x => x.IdPadre).HasColumnName("ID_ELAP_PADRE");

            //Relacion 1 to N
            builder
                .HasOne(x => x.Padre)
                .WithMany(x => x.Descendientes)
                .HasPrincipalKey(x => x.IdPadre);

            builder.HasOne(x => x.Aplicacion)
                    .WithMany(x => x.Elementos)
                    .HasForeignKey(x => x.IdAplicacion);

            builder.HasOne(x => x.TipoElementoAplicacion)
                    .WithMany(x => x.Elementos)
                    .HasForeignKey(x => x.CodigoTipoElemento);

            builder.HasMany(x => x.GruposPermisos)
                .WithOne(x => x.ElementoAplicacion)
                .HasForeignKey(x => x.IdElemento);
        }
    }
}
