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
    public class ElementoEntradaCalendarioConfiguration
    {
            public ElementoEntradaCalendarioConfiguration(EntityTypeBuilder<ElementoEntradaCalendarioEntity> builder)
            {
                builder
                    .ToTable("elec_elemento_encl")
                    .HasKey(x => new {x.IdEntradaCalendario, x.IdElemento });
                builder.Property(x => x.IdElemento).HasColumnName("ID_ELEM").IsRequired();
                builder.Property(x => x.IdEntradaCalendario).HasColumnName("ID_ENCL").IsRequired();
                builder.Property(x => x.Datos).HasColumnName("DATOS_ELEC");
    
                //Relaciones
                builder.HasOne(x => x.EntradaCalendario)
                    .WithMany(x => x.Elementos)
                    .HasForeignKey(x => x.IdEntradaCalendario);
        }
    }
}
