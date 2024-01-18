using lfvb.secure.domain.Entities.RelacionTipoElementoTipoPermiso;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones
{
    public class RelacionTipoElementoTipoPermisoConfiguration
    {

        public RelacionTipoElementoTipoPermisoConfiguration(EntityTypeBuilder<RelacionTipoElementoTipoPermisoEntity> builder)
        {
            builder
                .ToTable("TETP_TEAP_TPTE")
                .HasKey(x => new { x.CodigoTipoElemento, x.CodigoTipoPermiso });

            builder.Property(x => x.CodigoTipoElemento).HasColumnName("COD_TEAP").IsRequired();
            builder.Property(x => x.CodigoTipoPermiso).HasColumnName("COD_TPTE").IsRequired();

            builder.HasOne(x => x.TipoElemento)
                .WithMany(x => x.RelacionTiposPermisos)
                .HasForeignKey(x => x.CodigoTipoElemento);

            builder.HasOne(x => x.TipoPermiso)
                .WithMany(x => x.RelacionTiposElementos)
                .HasForeignKey(x => x.CodigoTipoPermiso);
        }
    }
}
