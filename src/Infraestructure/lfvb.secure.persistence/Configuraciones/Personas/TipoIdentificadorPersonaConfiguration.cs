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
    public class TipoIdentificadorPersonaConfiguration
    {
        public TipoIdentificadorPersonaConfiguration(EntityTypeBuilder<TipoIdentificadorPersonaEntity> builder)
        {
            builder
                .ToTable("tiid_tipo_identificador_persona")
                .HasKey(x => x.Codigo);


            builder.Property(x => x.Id).HasColumnName("ID_TIID");
            builder.Property(x => x.Codigo).HasColumnName("COD_TIID").HasMaxLength(36).IsRequired();
            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_TIID").HasMaxLength(60).IsRequired();

            builder.HasMany(x => x.Identificadores)
                .WithOne(i => i.TipoIdentificadorPersona)
                .HasForeignKey(i => i.CodigoTipoIdentificador);
        }   
    }
}
