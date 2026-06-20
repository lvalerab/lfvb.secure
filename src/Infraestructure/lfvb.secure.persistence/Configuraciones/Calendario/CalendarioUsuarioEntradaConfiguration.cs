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
    public class CalendarioUsuarioEntradaConfiguration
    {
        public CalendarioUsuarioEntradaConfiguration(EntityTypeBuilder<CalendarioUsuarioEntradasEntity> builder)
        {
            builder
                .ToTable("eccu_encl_caus")
                .HasKey(x => new {x.IdCalendarioUsuario, x.IdEntradaCalendario  });
            builder.Property(x => x.IdCalendarioUsuario).HasColumnName("ID_CAUS").IsRequired();
            builder.Property(x => x.IdEntradaCalendario).HasColumnName("ID_ENCL").IsRequired();   
            
            //Relaciones
            builder.HasOne(x => x.CalendarioUsuario)
                .WithMany(x => x.Entradas)
                .HasForeignKey(x => x.IdCalendarioUsuario);

            builder.HasOne(x => x.EntradaCalendario)
                .WithMany(x => x.Calendarios)
                .HasForeignKey(x => x.IdEntradaCalendario);

        }
    }
}
