using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lfvb.secure.domain.Entities.PropiedadElemento;

namespace lfvb.secure.persistence.Configuraciones
{
    public class PropiedadElementoConfiguration
    {
        public PropiedadElementoConfiguration(EntityTypeBuilder<PropiedadElementoEntity> builder)
        {
            builder
                .ToTable("elpr_elemento_propiedad")
                .HasKey(x => new { x.Id});


            builder.Property(x => x.IdElemento).HasColumnName("ID_ELEM").IsRequired();
            builder.Property(x => x.CodPropiedad).HasColumnName("COD_PROP").IsRequired().HasMaxLength(10);
            builder.Property(x => x.FechaValor).HasColumnName("FECHA_VALOR").IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Activo).HasColumnName("ACTIVO_VALOR").IsRequired().HasMaxLength(1).HasDefaultValue("N");
        
            builder.HasOne(x => x.Elemento)
                .WithMany(x => x.Propiedades)
                .HasForeignKey(x => x.IdElemento);

            builder.HasOne(x => x.Propiedad)
                .WithMany(x => x.PropiedadesElementos)
                .HasForeignKey(x => x.CodPropiedad);

            builder.HasMany(x => x.Valores)
                .WithOne(x => x.PropiedadElemento)
                .HasForeignKey(x => x.IdPropiedadElemento);

        }
    }
}
