using lfvb.secure.domain.Entities.Circuitos.EstadoElemento;
using lfvb.secure.domain.Entities.Circuitos.EstadoElementoSiguiente;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Circuitos
{
    public class EstadoElementoSiguienteConfiguration
    {
        public EstadoElementoSiguienteConfiguration(EntityTypeBuilder<EstadoElementoSiguienteEntity> builder)
        {
            builder
                .ToTable("ESES_ESTADOS_ELEMENTO_SIGUIENTE")
                .HasKey(x => x.Id);
            builder.Property(x =>x.Id).HasColumnName("ID_ESEL").IsRequired();
            builder.Property(x => x.IdSiguiente).HasColumnName("ID_ESEL_SIGUIENTE").IsRequired();
            builder.Property(x => x.IdUsuarioEnvio).HasColumnName("ID_USUA_ENVIO").IsRequired();
            builder.Property(x => x.Fecha).HasColumnName("FECHA_ENVIO").HasDefaultValue(DateTime.Now).IsRequired();  
            

            /*builder.HasOne(x => x.RelacionEstadoActual)
                   .WithOne(x=>x.RelacionEstadoActual)
                   .HasForeignKey<EstadoElementoEntity>(e=>e.Id);

            builder.HasOne(x => x.RelacionEstadoSiguiente)
                     .WithOne(x => x.RelacionEstadoSiguiente)                     
                     .HasForeignKey<EstadoElementoEntity>(x => x.Id);*/

            builder.HasOne(x => x.UsuarioEnvio)
                     .WithMany(u=>u.EnvioEstados)
                     .HasForeignKey(x => x.IdUsuarioEnvio);
        }
    }
}
