using lfvb.secure.domain.Entities.Circuitos.AccionTipoElemento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Circuitos
{
    public class AccionTipoElementoConfiguration
    {
        public AccionTipoElementoConfiguration(EntityTypeBuilder<AccionTipoElementoEntity> builder)
        {
            builder
                .ToTable("ACTI_ACCI_TIEL")
                .HasKey(x => new { x.Id,x.CodigoTipoElemento });

            builder.Property(x => x.Id).HasColumnName("ID_ACCI").IsRequired();  
            builder.Property(x => x.CodigoTipoElemento).HasColumnName("COD_TIEL").IsRequired();
            builder.Property(x => x.LlamarSW).HasColumnName("LLAMAR_SW").IsRequired().HasDefaultValue(false);
            builder.Property(x => x.PuntoAcceso).HasColumnName("PUNTO_ACCESO_SW");
            builder.Property(x => x.LlamarLibreriaNET).HasColumnName("LLAMAR_LIB_NET").IsRequired().HasDefaultValue(false);
            builder.Property(x => x.LibreriaNET).HasColumnName("LIBRERIA_NET");
            builder.Property(x => x.MetodoLibreriaNET).HasColumnName("METODO_NET");
            builder.Property(x => x.EsAccionUsuario).HasColumnName("ACCION_USUARIO").IsRequired().HasDefaultValue(false);
            builder.Property(x => x.CodigoAccionUsuario).HasColumnName("COD_ACUS");           
            
            
            builder.HasOne(x => x.TipoElemento).WithMany(t => t.Acciones).HasForeignKey(x => x.CodigoTipoElemento);
            builder.HasOne(x => x.Accion).WithMany(a=>a.AccionesTipoElemento).HasForeignKey(x => x.Id);
            builder.HasOne(x => x.AccionUsuario).WithMany(au => au.AccionesTipoElemento).HasForeignKey(x => x.CodigoAccionUsuario); 

        }
    }
}
