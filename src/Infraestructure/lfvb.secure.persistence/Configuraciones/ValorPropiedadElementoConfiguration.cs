using lfvb.secure.domain.Entities.ValorPropiedadElemento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones
{
    public class ValorPropiedadElementoConfiguration
    {
        public ValorPropiedadElementoConfiguration(EntityTypeBuilder<ValorPropiedadElementoEntity> builder)
        {
            builder
                .ToTable("vrep_valor_elpr")
                .HasKey(x => new { x.Id });

            builder.Property(x => x.Id).HasColumnName("ID_VREP").IsRequired();
            builder.Property(x => x.IdPropiedadElemento).HasColumnName("ID_ELPR").IsRequired(); 
            builder.Property(x => x.Texto).HasColumnName("VALOR_TEXTO");
            builder.Property(x => x.Numerico).HasColumnName("VALOR_NUMERICO");
            builder.Property(x => x.Fecha).HasColumnName("VALOR_FECHA");
            builder.Property(x => x.Booleano).HasColumnName("VALOR_BOOLEANO");
            builder.Property(x => x.NumericoMaximo).HasColumnName("VALOR_NUMERICO_MAX");
            builder.Property(x => x.FechaMaximo).HasColumnName("VALOR_FECHA_MAX");

            builder.HasOne(x => x.PropiedadElemento)
                .WithMany(x => x.Valores)
                .HasForeignKey(x => x.IdPropiedadElemento);
        }
    }
}
