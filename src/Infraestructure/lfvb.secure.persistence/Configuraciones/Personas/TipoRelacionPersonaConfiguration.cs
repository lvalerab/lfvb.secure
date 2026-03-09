using lfvb.secure.domain.Entities.Personas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Personas
{
    public class TipoRelacionPersonaConfiguration
    {
        public TipoRelacionPersonaConfiguration(EntityTypeBuilder<TipoRelacionPersonaEntity> builder)
        {
            builder
                .ToTable("trpr_tipo_relacion_persona")                
                .HasKey(x => x.Codigo);
            builder.Property(x => x.Id).HasColumnName("ID_TRPR");
            builder.Property(x => x.Codigo).HasColumnName("COD_TRPR").HasMaxLength(36).IsRequired();
            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_REPE").HasMaxLength(255).IsRequired();

            builder.HasMany(x => x.Relaciones)
                .WithOne(r => r.TipoRelacionPersona)
                .HasForeignKey(r => r.CodigoTipoRelacion);
        }
    }
}
