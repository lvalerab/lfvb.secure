using lfvb.secure.domain.Entities.Calendario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Calendario
{
    public class ParticipanteEntradaCalendarioConfiguration
    {
        public ParticipanteEntradaCalendarioConfiguration(EntityTypeBuilder<ParticipantesEntradaCalendarioEntity> builder)
        {
           builder
                .ToTable("paec_participantes_encl")
                .HasKey(x => x.Id);

            builder.Property(x=>x.Id).HasColumnName("ID_PAEC").IsRequired(); 
            builder.Property(x => x.IdEntradaCalendario).HasColumnName("ID_ENCL").IsRequired();
            builder.Property(x => x.IdElem).HasColumnName("ID_ELEM").IsRequired();
            builder.Property(x=>x.EMail).HasColumnName("MAIL_PAEC").IsRequired(false);

            //Relaciones
            builder.HasOne(x => x.EntradaCalendario)
                .WithMany(x => x.Participantes)
                .HasForeignKey(x => x.IdEntradaCalendario);

        }
    }
}
