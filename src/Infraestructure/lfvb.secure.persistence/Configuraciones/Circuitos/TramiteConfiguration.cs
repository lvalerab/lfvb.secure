using lfvb.secure.domain.Entities.Circuitos.Tramite;
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
    public class TramiteConfiguration
    {
        public TramiteConfiguration(EntityTypeBuilder<TramiteEntity> builder) 
        {
            builder
                .ToTable("TRAM_TRAMITE")
                .HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID_TRAM").IsRequired();
            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_TRAM");
            builder.Property(x => x.Descripcion).HasColumnName("DESCRIPCION_TRAM"); 
            builder.Property(x => x.Normativa).HasColumnName("NORMATIVA_TRAM"); 
        }
    }
}
