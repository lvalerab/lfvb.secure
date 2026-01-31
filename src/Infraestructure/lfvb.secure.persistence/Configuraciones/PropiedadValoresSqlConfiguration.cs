using lfvb.secure.domain.Entities.PropiedadValoresSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones
{
    public class PropiedadValoresSqlConfiguration
    {
        public PropiedadValoresSqlConfiguration(EntityTypeBuilder<PropiedadValoresSqlEntity> builder)
        {
            builder.ToTable("povs_prop_valores_sql")
                .HasKey(x => new { x.Codigo, x.Etiqueta});


            builder.Property(x => x.Codigo).HasColumnName("COD_PROP").IsRequired();
            builder.Property(x => x.Etiqueta).HasColumnName("ETIQUETA_POVS").IsRequired().HasMaxLength(255);
            builder.Property(x => x.FiltrarPorId).HasColumnName("FILTRO_POR_ID").IsRequired().HasMaxLength(1).HasDefaultValue("N");
            builder.Property(x => x.Sql).HasColumnName("SQL_POVS").IsRequired();

            builder.HasOne(x => x.Propiedad)
                .WithMany(x => x.ValoresSql).HasForeignKey(x=> x.Codigo);
        }
    }
}
