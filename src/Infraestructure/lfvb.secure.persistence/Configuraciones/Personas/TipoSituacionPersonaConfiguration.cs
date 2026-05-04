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
    public class TipoSituacionPersonaConfiguration
    {
        public TipoSituacionPersonaConfiguration(EntityTypeBuilder<TipoSituacionPersonaEntity> builder)
        {
            builder.ToTable("sitp_tipo_situacion_persona");
            builder.HasKey(x => x.Codigo);
            builder.Property(x => x.Codigo).HasColumnName("COD_SITP").HasMaxLength(10).IsRequired();
            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_SITP").HasMaxLength(60).IsRequired();
            builder.HasMany(x => x.SituacionPersonas)
                .WithOne(x => x.TipoSituacionPersona)
                .HasForeignKey(x => x.CodigoSituacion);
        }   
    }
}
