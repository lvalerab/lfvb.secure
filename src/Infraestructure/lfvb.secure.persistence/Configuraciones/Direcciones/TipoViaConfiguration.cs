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
    public class TipoViaConfiguration
    {
        public TipoViaConfiguration(EntityTypeBuilder<TipoViaEntity> builder)
        {
            builder
                .ToTable("tivi_tipo_via")
                .HasKey(x => x.Codigo); 

            builder.Property(x => x.Codigo).HasColumnName("COD_TIVI").HasMaxLength(10).IsRequired();  
            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_TIVI").HasMaxLength(100).IsRequired();

            builder.HasMany(x => x.Vias)
                .WithOne(v => v.TipoVia)
                .HasForeignKey(v => v.CodigoTipoVia);
        }
    }
}
