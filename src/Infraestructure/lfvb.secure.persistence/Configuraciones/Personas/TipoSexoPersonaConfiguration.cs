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
    public class TipoSexoPersonaConfiguration
    {
        public TipoSexoPersonaConfiguration(EntityTypeBuilder<TipoSexoPersonaEntity> entity)
        {
            entity.ToTable("tsex_tipo_sexo_persona");
            entity.HasKey(e => e.Codigo);
            entity.Property(e => e.Codigo).HasColumnName("COD_TSEX").HasMaxLength(10).IsRequired();
            entity.Property(e => e.Nombre).HasColumnName("NOMBRE_TSEX").HasMaxLength(60).IsRequired();
            entity.HasMany(e => e.Personas)
                  .WithOne(p => p.TipoSexo)
                  .HasForeignKey(p => p.CodigoSexo);
        }
    }
}
