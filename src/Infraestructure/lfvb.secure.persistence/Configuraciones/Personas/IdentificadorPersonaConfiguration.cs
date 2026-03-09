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
    public class IdentificadorPersonaConfiguration
    {
        public IdentificadorPersonaConfiguration(EntityTypeBuilder<IdentificadorPersonaEntity> builder)
        {
            builder
                .ToTable("idpe_identificador_persona")
                .HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID_IDPE").IsRequired();
            builder.Property(x => x.IdPersona).HasColumnName("ID_PERS").IsRequired();
            builder.Property(x => x.CodigoTipoIdentificador).HasColumnName("COD_TIID").HasMaxLength(36).IsRequired();
            builder.Property(x => x.Dato1).HasColumnName("DATO1_IDPE").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Dato2).HasColumnName("DATO2_IDPE").HasMaxLength(100);
            builder.Property(x => x.InicioVigencia).HasColumnName("FECHA_INICIO_VIGENCIA").IsRequired();
            builder.Property(x => x.FinVigencia).HasColumnName("FECHA_FIN_VIGENCIA").IsRequired();


            builder.HasOne(x => x.Persona)
                .WithMany(p => p.Identificadores)
                .HasForeignKey(x => x.IdPersona);


            builder.HasOne(x => x.TipoIdentificadorPersona)
                .WithMany(ti => ti.Identificadores)
                .HasForeignKey(x => x.CodigoTipoIdentificador);
        }
    }
}
