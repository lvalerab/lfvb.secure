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
    public class TipoPersonaConfiguration
    {
        public TipoPersonaConfiguration(EntityTypeBuilder<TipoPersonaEntity> builder)
        {
            builder.ToTable("tipe_tipo_persona")
                .HasKey(x => x.Codigo);
            builder.Property(x => x.Codigo).HasColumnName("COD_TIPE").HasMaxLength(3).IsRequired();
            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_TIPE").IsRequired();
            
            
            //Relacion 1 a muchos
            builder.HasMany(x => x.Personas)
                .WithOne(x => x.TipoPersona)
                .HasForeignKey(x => x.CodigoTipoPersona);
        }
    }
}
