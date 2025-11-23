using lfvb.secure.domain.Entities.Circuitos.EstadoElemento;
using lfvb.secure.domain.Entities.Circuitos.EstadoElementoSiguiente;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Circuitos
{
    public class EstadoElementoConfiguration
    {
        public EstadoElementoConfiguration(EntityTypeBuilder<EstadoElementoEntity> builder) 
        {

            builder
                .ToTable("ESEL_ESTADO_ELEMENTO")
                .HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID_ESEL").HasComputedColumnSql().IsRequired();
            builder.Property(x => x.IdElemento).HasColumnName("ID_ELEM");
            builder.Property(x => x.Fecha).HasColumnName("FECHA_ESEL").HasDefaultValue(DateTime.Now);
            builder.Property(x => x.IdUsuarioTramitador).HasColumnName("ID_USUA_TRAMITADOR");
            builder.Property(x => x.CodEstado).HasColumnName("COD_ESTA");
            builder.Property(x => x.IdCircuito).HasColumnName("ID_CIRC");
            builder.Property(x => x.IdPaso).HasColumnName("ID_PASO");


            builder.HasOne(x => x.Elemento).WithMany(e => e.Estados).HasForeignKey(x => x.IdElemento);
            builder.HasOne(x => x.Estado).WithMany(e => e.EstadosElemento).HasForeignKey(x => x.CodEstado);
            builder.HasOne(x => x.Circuito).WithMany(c => c.ElementosEstados).HasForeignKey(x => x.IdCircuito);
            builder.HasOne(x => x.UsuarioTramitador).WithMany(u => u.Tramitadores).HasForeignKey(x => x.IdUsuarioTramitador);
            builder.HasOne(x => x.Paso).WithMany(p => p.EstadosElementos).HasForeignKey(x => x.IdPaso);

            builder.HasOne(x => x.RelacionEstadoActual)
                   .WithOne(x => x.RelacionEstadoActual)
                   .HasForeignKey<EstadoElementoSiguienteEntity>(e => e.Id); 

            builder.HasOne(x => x.RelacionEstadoSiguiente)
                     .WithOne(x => x.RelacionEstadoSiguiente)
                     .HasForeignKey<EstadoElementoSiguienteEntity>(x => x.IdSiguiente);  
        }
    }
}
