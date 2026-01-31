using lfvb.secure.domain.Entities.i18N;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.i18N
{
    public class IdiomaConfiguration
    {
        public IdiomaConfiguration(EntityTypeBuilder<IdiomaEntity> builder) {
            
            builder.ToTable("idio_idioma")
                     .HasKey(e => e.Codigo);

            builder.Property(e => e.Codigo).HasColumnName("COD_IDIO").HasMaxLength(10);
            builder.Property(e => e.Id).HasColumnName("ID_IDIO");
            builder.Property(e => e.Nombre).HasColumnName("NOMBRE_IDIO").HasMaxLength(255);

            builder.HasMany(e => e.Agrupaciones)
                   .WithOne(a => a.Idioma)
                   .HasForeignKey(a => a.Codigo);

            builder.HasMany(e => e.AgrupacionesPertenecientes)
                     .WithOne(a => a.IdiomaRelacionado)
                     .HasForeignKey(a => a.CodigoIdiomaRelacionado);    
        }
    }
}
