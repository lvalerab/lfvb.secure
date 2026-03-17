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
    public class PersonaConfiguration
    {
        public PersonaConfiguration(EntityTypeBuilder<PersonaEntity> builder)
        {
            builder
                .ToTable("pers_persona")
                .HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID_PERS").IsRequired();
            builder.Property(x => x.CodigoTipoPersona).HasColumnName("COD_TIPE").HasMaxLength(3).IsRequired();
            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_PERS").HasMaxLength(255).IsRequired();
            builder.Property(x => x.Apellido1).HasColumnName("APELLIDO1_PERS").HasMaxLength(255).IsRequired();
            builder.Property(x => x.Apellido2).HasColumnName("APELLIDO2_PERS");

            builder.HasOne(x => x.TipoPersona)
                .WithMany(x => x.Personas)
                .HasForeignKey(x => x.CodigoTipoPersona);

           builder.HasMany(x => x.Elementos)
                .WithOne(x => x.Persona)
                .HasForeignKey(x => x.IdPersona);

            builder.HasMany(x => x.Identificadores)
                .WithOne(x => x.Persona)
                .HasForeignKey(x => x.IdPersona);   
            
            builder.HasMany(x => x.Relaciones)
                .WithOne(x => x.Persona)
                .HasForeignKey(x => x.IdPersona);

            builder.HasMany(x => x.Relacionados)
                .WithOne(x => x.PersonaRelacionada)
                .HasForeignKey(x => x.IdPersonaRelacionada);
        }
    }
}
