using lfvb.secure.domain.Entities.Circuitos.Paso;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Circuitos
{
    public class PasoConfiguration
    {
        public PasoConfiguration(EntityTypeBuilder<PasoEntity> builder) 
        { 
            builder
                .ToTable("PASO_PASOS")
                .HasKey(x=>x.Id);

            builder.Property(x => x.Id).HasColumnName("ID_PASO").IsRequired();  
            builder.Property(x => x.IdCircuito).HasColumnName("ID_CIRC").IsRequired(); 
            builder.Property(x=>x.CodEstado).HasColumnName("COD_ESTA").IsRequired();
            builder.Property(x => x.CodEstadoSiguiente).HasColumnName("COD_ESTA_SIG");
            builder.Property(x => x.IdCircuitoSiguiente).HasColumnName("ID_CIRC_SIG");

            builder.HasOne(x => x.Estado).WithMany(e => e.Pasos).HasForeignKey(x => x.CodEstado);
            builder.HasOne(x => x.Circuito).WithMany(c => c.Pasos).HasForeignKey(x => x.IdCircuito);
            builder.HasOne(x => x.EstadoSiguiente).WithMany().HasForeignKey(x => x.CodEstadoSiguiente);
            builder.HasOne(x => x.CircuitoSiguiente).WithMany().HasForeignKey(x => x.IdCircuitoSiguiente);

            builder.HasMany(x => x.PermisosGrupos).WithOne(pg => pg.Paso).HasForeignKey(x => x.IdPaso);
            builder.HasMany(x => x.PermisoUsuarios).WithOne(pu => pu.Paso).HasForeignKey(x => x.IdPaso);
            builder.HasMany(x => x.Acciones).WithOne(acc => acc.Paso).HasForeignKey(x => x.IdPaso);
        }
       
    }
}
