using lfvb.secure.domain.Entities.i18N;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.i18N
{
    public class CampoTextoConfiguration
    {
        public CampoTextoConfiguration(EntityTypeBuilder<CampoTextoEntity> builder)
        {
            builder.ToTable("cmtx_campo_texto")
                   .HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("ID_CMTX");
            builder.Property(e => e.IdColeccion).HasColumnName("ID_CLTX");
            builder.Property(e => e.Nombre).HasColumnName("NOMBRE_CMTX").HasMaxLength(255).IsUnicode(true);

            builder.HasOne(e => e.Coleccion)
                   .WithMany(c => c.Campos)
                   .HasForeignKey(e => e.IdColeccion);  
        }
    }
}
