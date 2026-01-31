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
    public class ColumnaTextoIdiomaConfiguration
    {
        public ColumnaTextoIdiomaConfiguration(EntityTypeBuilder<ColumnaTextoIdiomaEntity> builder)
        {
            builder.ToTable("CTID_COLUMNA_TEXTO_IDIOMA")
                   .HasKey(e => new { e.Id, e.CodIdioma, e.CodIdiomaRelacionado });

            builder.Property(e => e.Id).HasColumnName("ID_TEXT");
            builder.Property(e => e.CodIdioma).HasColumnName("COD_IDIO").HasMaxLength(10);  
            builder.Property(e => e.CodIdiomaRelacionado).HasColumnName("COD_IDIO_RELA").HasMaxLength(10);  
            builder.Property(e => e.Contenido).HasColumnName("TEXT_CTID").IsUnicode(true);

            builder.HasOne(e => e.Texto)
                   .WithMany(t => t.ColumnasTextosIdiomas)
                   .HasForeignKey(e => e.Id);

            builder.HasOne(e => e.AgrupacionIdioma)
                     .WithMany(i => i.ColumnasTexto)
                     .HasForeignKey(e => new { e.CodIdioma, e.CodIdiomaRelacionado });
        }
    }
}
