using lfvb.secure.domain.Entities.Elemento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones
{
    public class ElementoConfiguration
    {
        public ElementoConfiguration(EntityTypeBuilder<ElementoEntity> builder)
        {
            builder
                .ToTable("ELEM_ELEMENTO")
                .HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID_ELEM").IsRequired();

        }
    }
}
