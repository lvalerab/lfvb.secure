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
    public class TextoConfiguration
    {
        public TextoConfiguration(EntityTypeBuilder<TextoEntity> builder)
        {
            builder.ToTable("text_texto")
                   .HasKey(e => e.Id);


            builder.Property(e => e.Id).HasColumnName("ID_TEXT");

            builder.HasMany(e => e.TextosIdiomas)
                   .WithOne(o => o.Texto)
                   .HasForeignKey(o => o.Id);

            builder.HasMany(e => e.ColumnasTextosIdiomas)
                    .WithOne(o => o.Texto)
                    .HasForeignKey(o => o.Id);  

            builder.HasMany(e => e.Variables)
                     .WithOne(v => v.Texto)
                     .HasForeignKey(v => v.IdTexto);

            builder.HasMany(e => e.Opciones)
                        .WithOne(o => o.Texto)
                        .HasForeignKey(o => o.IdTexto); 
        }
    }
}
