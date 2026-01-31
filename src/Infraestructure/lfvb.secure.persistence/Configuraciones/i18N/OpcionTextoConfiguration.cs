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
    public class OpcionTextoConfiguration
    {
        public OpcionTextoConfiguration(EntityTypeBuilder<OpcionTextoEntity> builder)
        {
            builder.ToTable("optx_opcion_campo_texto")
                   .HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("ID_OPTX");
            builder.Property(e => e.IdTexto).HasColumnName("ID_TEXT");
            builder.Property(e => e.Opcion).HasColumnName("OPCION_OPTX").HasMaxLength(255).IsUnicode(true);

            builder.HasOne(e => e.Texto)
                   .WithMany(t => t.Opciones)
                   .HasForeignKey(e => e.IdTexto);
        }
    }
}
