using lfvb.secure.domain.Entities.Circuitos.Estado;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Circuitos
{
    public class EstadoConfiguration
    {
        public EstadoConfiguration(EntityTypeBuilder<EstadoEntity> builder) 
        { 
            builder
                .ToTable("ESTA_ESTADO")
                .HasKey(x=>x.Codigo);   

            builder.Property(x => x.Codigo).HasColumnName("COD_ESTA").IsRequired();
            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_ESTA").IsRequired().HasMaxLength(60);
            builder.Property(x => x.Descripcion).HasColumnName("DESCRIPCION_ESTA").HasMaxLength(200);

            builder.HasMany(x => x.Pasos)
                .WithOne(e => e.Estado)
                .HasForeignKey(e => e.CodEstado);

            builder.HasMany(x => x.PasosSiguientes)
                .WithOne(e => e.EstadoSiguiente)
                .HasForeignKey(e => e.CodEstadoSiguiente);
        }
    }
}
