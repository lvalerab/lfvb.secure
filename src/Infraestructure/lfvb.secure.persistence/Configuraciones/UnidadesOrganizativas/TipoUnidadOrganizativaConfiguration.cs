using lfvb.secure.domain.Entities.TipoUnidadOrganizativa;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.UnidadesOrganizativas
{
    public class TipoUnidadOrganizativaConfiguration
    {
        public TipoUnidadOrganizativaConfiguration(EntityTypeBuilder<TipoUnidadOrganizativaEntity> builder)
        {
            builder
                .ToTable("tuno_tipo_unor")
                .HasKey(tuo => tuo.Codigo); 

            builder.Property(tuo => tuo.Codigo)
                .HasColumnName("COD_TUNO")
                .IsRequired();
            builder.Property(tuo => tuo.Nombre)
                .HasColumnName("NOMBRE_TUNO")
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(tuo => tuo.Descripcion)
                .HasColumnName("DESCRIPCION_TUNO")                
                .IsRequired(false);

            builder.HasMany(tuo => tuo.UnidadesOrganizativas)
                .WithOne(uo => uo.TipoUnidadOrganizativa)
                .HasForeignKey(uo => uo.CodTuno);   
        }
    }
}
