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
    public class AgrupacionIdiomaConfiguration
    {
        public AgrupacionIdiomaConfiguration(EntityTypeBuilder<AgrupacionIdiomaEntity> builder)
        {
            builder.ToTable("agid_agrupacion_idioma")
                     .HasKey(e => new { e.Codigo, e.CodigoIdiomaRelacionado });

            builder.Property(e => e.Codigo).HasColumnName("COD_IDIO");
            builder.Property(e => e.CodigoIdiomaRelacionado).HasColumnName("COD_IDIO_RELA");
            builder.Property(e => e.Orden).HasColumnName("ORDEN_AGID").HasDefaultValue(0);

            builder.HasOne(e => e.Idioma)
                   .WithMany(i => i.Agrupaciones)
                   .HasForeignKey(e => e.Codigo);

            builder.HasOne(e => e.IdiomaRelacionado)
                     .WithMany(i => i.AgrupacionesPertenecientes)
                     .HasForeignKey(e => e.CodigoIdiomaRelacionado);
        }
    }
}
