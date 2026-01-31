using lfvb.secure.domain.Entities.Circuitos.GrupoAdministradorCircuito;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Circuitos
{
    public class GrupoAdministradorCircuitoConfiguration
    {
        public GrupoAdministradorCircuitoConfiguration(EntityTypeBuilder<GrupoAdministradorCircuitoEntity> builder)
        {
            builder
                .ToTable("CIGU_CIRC_GUAP")
                .HasKey(x => new { x.IdGuap, x.IdCircuito });
            
            builder.Property(x => x.IdGuap).HasColumnName("ID_GUAP").IsRequired();  
            builder.Property(x => x.IdCircuito).HasColumnName("ID_CIRC").IsRequired();  

            //Relacione de 1 a muchos

            builder.HasOne(x => x.GrupoUsuarioAplicacion)
                .WithMany(x => x.CircuitosAdministrados)
                .HasForeignKey(x => x.IdGuap);

            builder.HasOne(x => x.Circuito)
                .WithMany(x => x.GruposAdministradores)
                .HasForeignKey(x => x.IdCircuito);

        }
    }
}
