using lfvb.secure.domain.Entities.TipoPermisoElementoAplicacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones
{
    public class TipoPermisoElementoAplicacionConfiguration
    {
        public TipoPermisoElementoAplicacionConfiguration(EntityTypeBuilder<TipoPermisoElementoAplicacionEntity> builder)
        {
            builder
                .ToTable("TPTE_TIPO_PERMISO_TEAP")
                .HasKey(x => x.Codigo);

            builder.Property(x => x.Codigo).HasColumnName("COD_TPTE").IsRequired();
            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_TPTE").IsRequired();
            builder.Property(x => x.Prioridad).HasColumnName("PRIORIDAD_TPTE").HasDefaultValue(0);

            builder.HasMany(x => x.RelacionTiposElementos)
                   .WithOne(x => x.TipoPermiso)
                   .HasForeignKey(x => x.CodigoTipoPermiso);

            builder.HasMany(x => x.RelacionElementosGrupos)
                .WithOne(x => x.TipoPermiso)
                .HasForeignKey(x => x.CodigoTipoPermiso);
        }
    }
}
