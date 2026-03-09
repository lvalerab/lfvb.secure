using lfvb.secure.domain.Entities.Direcciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Direcciones
{
    public class TipoCodigoGestionTerritorialConfiguration
    {
        public TipoCodigoGestionTerritorialConfiguration(EntityTypeBuilder<TipoCodigoGestionTerritorialEntity> builder)
        {
            builder
                .ToTable("tpct_tipo_codigo_gestion_territorial")
                .HasKey(x => x.Codigo);
            builder.Property(x => x.Id).HasColumnName("ID_TPCT").IsRequired();
            builder.Property(x => x.Codigo).HasColumnName("COD_TPCT").HasMaxLength(36).IsRequired();
            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_TPCT").HasMaxLength(255).IsRequired();

            builder.HasMany(x => x.Codigos)
                .WithOne(et => et.TipoCodigoGestionTerritorial)
                .HasForeignKey(et => et.CodigoTipo);
        }
    }
}
