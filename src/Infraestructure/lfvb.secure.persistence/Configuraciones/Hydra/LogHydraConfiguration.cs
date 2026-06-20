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
    public class LogHydraConfiguration
    {
        public LogHydraConfiguration(EntityTypeBuilder<LogHydraEntity> builder)
        {
            builder
                .ToTable("LGHD_LOG_HYDRA")
                .HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID_LGHD").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.IdHydra).HasColumnName("ID_HYDR").IsRequired();
            builder.Property(x => x.Fecha).HasColumnName("FECHA_LGHD").IsRequired();
            builder.Property(x => x.Tipo).HasColumnName("IMPORTANCIA_LGHD").IsRequired();
            builder.Property(x => x.Mensaje).HasColumnName("MENSAJE_LGHD").IsRequired();
            builder.Property(x => x.Datos).HasColumnName("DATOS_LGHD").IsRequired(false);

            builder.HasOne(x => x.Hydra)
                .WithMany(x=>x.Logs)
                .HasForeignKey(x => x.IdHydra);
        }
    }
}
