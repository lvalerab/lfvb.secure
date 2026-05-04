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
    public class SituacionPersonaConfiguration
    {
        public SituacionPersonaConfiguration(EntityTypeBuilder<SituacionPersonaEntity> builder)
        {
            builder.ToTable("sipe_situacion_persona");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID_SIPE").IsRequired();
            builder.Property(x => x.IdPersona).HasColumnName("ID_PERS").IsRequired();
            builder.Property(x => x.CodigoSituacion).HasColumnName("COD_SITP").HasMaxLength(10).IsRequired();
            builder.Property(x => x.FechaDesde).HasColumnName("FECHA_INICIO").IsRequired();
            builder.Property(x => x.FechaHasta).HasColumnName("FECHA_FIN").IsRequired(false);
            builder.Property(x => x.Observaciones).HasColumnName("OBSERVACIONES_SIPE").IsRequired(false);
            builder.HasOne(x => x.Persona)
                .WithMany(p => p.Situaciones)
                .HasForeignKey(x => x.IdPersona);
            builder.HasOne(x => x.TipoSituacionPersona)
                .WithMany(t => t.SituacionPersonas)
                .HasForeignKey(x => x.CodigoSituacion);
        }
    }
}
