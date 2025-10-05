using lfvb.secure.domain.Entities.Circuitos.TipoElementoCircuito;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Circuitos
{
    public class TipoElementoCircuitoConfiguration
    {
        public TipoElementoCircuitoConfiguration(EntityTypeBuilder<TipoElementoCircuitoEntity> builder)
        {
            builder
                .ToTable("TICI_TIEL_CIRC")
                .HasKey(x => new { x.IdCircuito, x.CodigoTipoElemento });

            builder.Property(x => x.IdCircuito).HasColumnName("ID_CIRC").IsRequired();
            builder.Property(x => x.CodigoTipoElemento).HasColumnType("COD_TIEL").IsRequired();

            //Relacion 1 a muchos

            builder
                .HasOne(x => x.Circuito)
                .WithMany(x => x.RelacionTiposElementos)
                .HasForeignKey(x => x.IdCircuito);


            builder
                .HasOne(x => x.TipoElemento)
                .WithMany(x => x.RelacionCircuitos)
                .HasForeignKey(x => x.CodigoTipoElemento);
        }

    }
}
