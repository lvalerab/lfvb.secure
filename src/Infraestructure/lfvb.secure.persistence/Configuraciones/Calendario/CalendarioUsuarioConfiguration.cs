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
    public class CalendarioUsuarioConfiguration
    {
        public CalendarioUsuarioConfiguration(EntityTypeBuilder<CalendarioUsuarioEntity> builder)
        {
            builder
                .ToTable("caus_calendario_usuario")
                .HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID_CAUS").IsRequired();
            builder.Property(x => x.IdUsuario).HasColumnName("ID_USUA").IsRequired();
            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_CAUS").IsRequired();

            builder.HasMany(x => x.Entradas)
                .WithOne(x => x.CalendarioUsuario)
                .HasForeignKey(x => x.IdCalendarioUsuario);

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.CalendariosEntradas)
                .HasForeignKey(x => x.IdUsuario);
           
        }
    }
}
