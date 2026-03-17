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
    public class CodigoGestionTerritorialConfiguration
    {
        public CodigoGestionTerritorialConfiguration(EntityTypeBuilder<CodigoGestionTerritorialEntity> builder)
        {
            builder
                .ToTable("cgtr_codigos_gestion_territorial")
                .HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID_CGTR").IsRequired();            
            builder.Property(x => x.IdElemento).HasColumnName("ID_ELEM").IsRequired();
            builder.Property(x => x.CodigoTipo).HasColumnName("COD_TPCT").IsRequired();
            builder.Property(x => x.Codigo).HasColumnName("CODIGO").IsRequired();

            builder.HasOne(x => x.TipoCodigoGestionTerritorial)
                .WithMany(t => t.Codigos)
                .HasForeignKey(x => x.CodigoTipo);
        }
    }
}
