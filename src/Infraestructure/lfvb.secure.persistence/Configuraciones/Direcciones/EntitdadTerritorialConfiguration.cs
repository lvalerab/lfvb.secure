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
    public class EntitdadTerritorialConfiguration
    {
        public EntitdadTerritorialConfiguration(EntityTypeBuilder<EntitdadTerritorialEntity> builder)
        {
            builder
                .ToTable("ente_entidad_territorial")
                .HasKey(x => x.Id); 
    
            builder.Property(x => x.Id).HasColumnName("ID_ENTE").IsRequired();
            builder.Property(x => x.IdPadre).HasColumnName("ID_ENTE_PADRE");
            builder.Property(x => x.CodigoTipoEntidad).HasColumnName("COD_TNTE").HasMaxLength(10).IsRequired();  
            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_ENTE").HasMaxLength(255).IsRequired();
    
            builder.HasOne(x => x.Padre)
                .WithMany(x => x.Hijos)
                .HasForeignKey(x => x.IdPadre);
    
            builder.HasOne(x => x.TipoEntidad)
                .WithMany(te=>te.Entidades)
                .HasForeignKey(x => x.CodigoTipoEntidad);

            builder.HasMany(x => x.Calles)
                .WithOne(c => c.EntidadTerritorial)
                .HasForeignKey(c => c.IdEntidadTerritorial);

            builder.HasMany(x => x.DireccionesNoNormalizadas)
                .WithOne(d => d.EntidadTerritorial)
                .HasForeignKey(d => d.IdEntidadTerritorial);
        }
    }
}
