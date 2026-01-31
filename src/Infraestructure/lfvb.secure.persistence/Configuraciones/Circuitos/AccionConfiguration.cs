using lfvb.secure.domain.Entities.Circuitos.Accion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Circuitos
{
    public class AccionConfiguration
    {
        public AccionConfiguration(EntityTypeBuilder<AccionEntity> builder) 
        {
            builder
                .ToTable("ACCI_ACCION")
                .HasKey(x=>x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ID_ACCI")
                .IsRequired();

            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_ACCI").IsRequired();

            builder.HasMany(x => x.AccionesTipoElemento)
                .WithOne(a => a.Accion)
                .HasForeignKey(a => a.Id);
            builder.HasMany(x => x.Pasos)
                .WithOne(ps => ps.Accion)
                .HasForeignKey(x => x.IdAccion);
        }
    }
}
