using lfvb.secure.domain.Entities.Hydra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Hydra
{
    public class HydraConfiguration
    {
        public HydraConfiguration(EntityTypeBuilder<HydraEntity> builder)
        {
            builder
                .ToTable("HYDR_HYDRAS")
                .HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID_HYDR").IsRequired();
            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_HYDR").IsRequired();
            builder.Property(x => x.IdUsuaProp).HasColumnName("ID_USUA_PROP");
            builder.Property(x => x.IdUsuaEjec).HasColumnName("ID_USUA_EJEC");
            //Relaciones
            builder.HasOne(x => x.Propietario)
                .WithMany(x=>x.HydraPropietario)
                .HasForeignKey(x => x.IdUsuaProp);

            builder.HasOne(x => x.Ejecutor)
                .WithMany(x=>x.HydraEjecutor)
                .HasForeignKey(x => x.IdUsuaEjec);
        }
    }
}
