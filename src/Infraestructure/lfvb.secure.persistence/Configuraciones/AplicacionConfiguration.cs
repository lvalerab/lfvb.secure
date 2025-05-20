using lfvb.secure.domain.Entities.Aplicacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones
{
    public class AplicacionConfiguration
    {
        public AplicacionConfiguration(EntityTypeBuilder<AplicacionEntity> buider)
        {
            buider
                .ToTable("APLI_APLICACIONES")
                .HasKey(x => x.Id);

            buider.Property(x => x.Id).HasColumnName("ID_APLI").IsRequired();
            buider.Property(x => x.Codigo).HasColumnName("COD_APLI").IsRequired();
            buider.Property(x => x.Nombre).HasColumnName("NOMBRE_APLI").IsRequired();

            //Relacion 1 a muchos
            buider.HasMany(x => x.Grupos)
                .WithOne(x => x.Aplicacion)
                .HasForeignKey(x => x.IdAplicacion);

            buider.HasMany(x => x.Elementos)
                .WithOne(x => x.Aplicacion)
                .HasForeignKey(x => x.IdAplicacion);
        }
    }
}
