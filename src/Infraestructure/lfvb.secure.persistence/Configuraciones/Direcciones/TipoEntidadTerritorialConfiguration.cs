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
    public class TipoEntidadTerritorialConfiguration
    {
        public TipoEntidadTerritorialConfiguration(EntityTypeBuilder<TipoEntidadTerritorialEntity> builder)
        {
            builder
                .ToTable("tnte_tipo_entidad_territorial")
                .HasKey(x => x.Codigo); 

            builder.Property(x => x.Id).HasColumnName("ID_TNTE");  
            builder.Property(x => x.Codigo).HasColumnName("COD_TNTE").HasMaxLength(10).IsRequired();  
            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_TNTE").HasMaxLength(100).IsRequired();

        }
    }
}
