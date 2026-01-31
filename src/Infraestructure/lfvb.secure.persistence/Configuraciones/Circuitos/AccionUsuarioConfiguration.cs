using lfvb.secure.domain.Entities.Circuitos.AccionUsuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Circuitos
{
    public class AccionUsuarioConfiguration
    {
        public AccionUsuarioConfiguration(EntityTypeBuilder<AccionUsuarioEntity> builder)  
        {
            builder
                .ToTable("ACUS_ACCION_USUARIO")
                .HasKey(x => x.Codigo); 

            builder.Property(x => x.Codigo).HasColumnName("COD_ACUS").IsRequired(); 
            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_ACUS").IsRequired();


            builder.HasMany(x => x.AccionesTipoElemento)
                .WithOne(a => a.AccionUsuario)
                .HasForeignKey(a => a.CodigoAccionUsuario);
        }
    }
}
