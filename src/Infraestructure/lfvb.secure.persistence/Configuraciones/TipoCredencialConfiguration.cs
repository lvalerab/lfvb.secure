using lfvb.secure.domain.Entities.TipoCredencial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones
{
    /// <summary>
    /// Configuracion para la entidad TipoCredencial
    /// </summary>
    public class TipoCredencialConfiguration
    {

        public TipoCredencialConfiguration(EntityTypeBuilder<TipoCredencialEntity> entityBuilder)
        {
            entityBuilder
                .ToTable("trcr_tipo_credencial")
                .HasKey(x => x.Codigo);
            entityBuilder.Property(x => x.Codigo).HasColumnName("COD_TRCR");
            entityBuilder.Property(x => x.Nombre).HasColumnName("NOMBRE_TRCR").IsRequired();
            entityBuilder.Property(x => x.ActivoDesde).HasColumnName("ACTIVO_DESDE_TRCR").IsRequired();
            entityBuilder.Property(x => x.ActivoHasta).HasColumnName("ACTIVO_HASTA_TRCR");

            //Relaciones 1 to N

            entityBuilder.HasMany(x => x.Credenciales)
                         .WithOne(x => x.TipoCredencial)
                         .HasForeignKey(x => x.CodigoTipoCredencial);
        }
    }
}
