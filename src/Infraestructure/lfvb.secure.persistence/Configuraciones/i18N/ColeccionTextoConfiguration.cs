using lfvb.secure.domain.Entities.i18N;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.i18N
{
    public class ColeccionTextoConfiguration
    {
        public ColeccionTextoConfiguration(EntityTypeBuilder<ColeccionTextoEntity> builder) 
        {
            builder.ToTable("cltx_coleccion_texto")
                   .HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("ID_CLTX");
            builder.Property(e => e.Nombre).HasColumnName("NOMBRE_CLTX").HasMaxLength(255); 
            builder.Property(e => e.Descripcion).HasColumnName("DETALLE_CLTX").IsUnicode(true);


            builder.HasMany(e => e.Campos)
                   .WithOne(c => c.Coleccion)
                   .HasForeignKey(c => c.IdColeccion);
        }
    }
}
