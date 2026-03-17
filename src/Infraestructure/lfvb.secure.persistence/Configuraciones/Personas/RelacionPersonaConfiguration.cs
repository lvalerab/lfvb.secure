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
    public class RelacionPersonaConfiguration
    {
        public RelacionPersonaConfiguration(EntityTypeBuilder<RelacionPersonaEntity> builder)
        {
            builder
                .ToTable("repe_relacion_persona")
                .HasKey(x => new { x.IdPersona, x.CodigoTipoRelacion, x.IdPersonaRelacionada });
            builder.Property(x => x.IdPersona).HasColumnName("ID_PERS").IsRequired();
            builder.Property(x => x.IdPersonaRelacionada).HasColumnName("ID_PERS_RELA").IsRequired();
            builder.Property(x => x.CodigoTipoRelacion).HasColumnName("COD_TRPR").HasMaxLength(36).IsRequired();
            builder.Property(x => x.InicioVigencia).HasColumnName("FECHA_INICIO_REPE").IsRequired();
            builder.Property(x => x.FinVigencia).HasColumnName("FECHA_FIN_REPE");
            builder.Property(x => x.Observaciones).HasColumnName("OBSERVACIONES").IsUnicode(true);


            builder.HasOne(x => x.Persona)
                .WithMany(p => p.Relaciones)
                .HasForeignKey(x => x.IdPersona);

            builder.HasOne(x => x.PersonaRelacionada)
                .WithMany(p => p.Relacionados)
                .HasForeignKey(x => x.IdPersonaRelacionada);


            builder.HasOne(x => x.TipoRelacionPersona)
                .WithMany(tr => tr.Relaciones)
                .HasForeignKey(x => x.CodigoTipoRelacion);
        }
    }
}
