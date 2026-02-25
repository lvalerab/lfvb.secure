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
    public class TextoIdiomaConfiguration
    {
        public TextoIdiomaConfiguration(EntityTypeBuilder<TextoIdiomaEntity> builder)
        {
            builder.ToTable("teid_texto_idioma")
                   .HasKey(e => new { e.Id, e.CodIdioma });

            builder.Property(e => e.Id).HasColumnName("ID_TEXT");
            builder.Property(e => e.CodIdioma).HasColumnName("COD_IDIO").HasMaxLength(10);
            builder.Property(e => e.Contenido).HasColumnName("TEXTO_TEID").IsUnicode(true);
            
            
            builder.HasOne(e => e.Texto)
                   .WithMany(t => t.TextosIdiomas)
                   .HasForeignKey(e => e.Id);

            builder.HasOne(e => e.Idioma)
                     .WithMany(i => i.Textos)
                     .HasForeignKey(e => e.CodIdioma);
        }
    }
}
