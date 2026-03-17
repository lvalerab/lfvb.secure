using lfvb.secure.domain.Entities.Personas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Personas
{
    public class ElementoPersonaConfiguration
    {
        public ElementoPersonaConfiguration(EntityTypeBuilder<ElementoPersonaEntity> builder) 
        { 
            builder
                .ToTable("elpe_elemento_persona")
                .HasKey(x => new { x.IdPersona, x.IdElemento });

            builder.Property(x => x.IdPersona).HasColumnName("ID_PERS").IsRequired();
            builder.Property(x => x.IdElemento).HasColumnName("ID_ELEM").IsRequired();

            builder.HasOne(x => x.Persona)
                .WithMany(x => x.Elementos)
                .HasForeignKey(x => x.IdPersona);

            builder.HasOne(x => x.Elemento)
                .WithMany(e=>e.ElementosPersona)
                .HasForeignKey(x => x.IdElemento);
        }
    }
}
