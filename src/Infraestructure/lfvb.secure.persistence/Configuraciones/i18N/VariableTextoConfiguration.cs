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
    public class VariableTextoConfiguration
    {
        public VariableTextoConfiguration(EntityTypeBuilder<VariableTextoEntity> builder)
        {
            builder.ToTable("vrtx_variable_texto")
                    .HasKey(e => e.Id);


            builder.Property(e => e.Id).HasColumnName("ID_VRTX");
            builder.Property(e => e.IdTexto).HasColumnName("ID_TEXT");
            builder.Property(e => e.Variable).HasColumnName("VARIABLE_VRTX").HasMaxLength(255).HasDefaultValue("[<%VARIABLE%>]").IsUnicode(true);

            builder.HasOne(e => e.Texto)
                   .WithMany(t => t.Variables)
                   .HasForeignKey(e => e.IdTexto);  
        }   
    }
}
