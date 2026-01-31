using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Circuitos
{
    public class BandejaTramiteConfiguration
    {
        public BandejaTramiteConfiguration(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<lfvb.secure.domain.Entities.Circuitos.BandejaTramite.BandejaTramiteEntity> builder)
        {
            builder
                .ToTable("batr_bandeja_tramite")
                .HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID_BATR").IsRequired();
            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_BATR").IsRequired();
            builder.Property(x => x.Descripcion).HasColumnName("DESCRIPCION_BATR");


            builder.HasMany(x => x.Pasos).WithOne(p => p.Bandeja).HasForeignKey(x => x.IdBandeja);
        }   
    }
}
