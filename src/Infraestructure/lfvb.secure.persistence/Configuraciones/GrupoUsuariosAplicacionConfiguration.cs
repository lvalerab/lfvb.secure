using lfvb.secure.domain.Entities.GrupoUsuarioAplicacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones
{
    public class GrupoUsuariosAplicacionConfiguration
    {
        public GrupoUsuariosAplicacionConfiguration(EntityTypeBuilder<GrupoUsuariosAplicacionEntity> builder)
        {
            builder
                .ToTable("GUAP_GRUPO_USUARIO_APLICACION")
                .HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID_GUAP").IsRequired();
            builder.Property(x => x.IdAplicacion).HasColumnName("ID_APLI");
            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_GUAP").IsRequired();
            builder.Property(x => x.IdPadre).HasColumnName("ID_GUAP_PADRE");

            //Relaciones 1 to N
            builder.HasOne(x => x.Padre)
                .WithMany(x => x.Hijos)
                .HasForeignKey(x => x.IdPadre);

            builder.HasOne(x => x.Aplicacion)
                .WithMany(x => x.Grupos)
                .HasForeignKey(x => x.IdAplicacion);

            builder.HasMany(x => x.RelacionUsuarios)
                .WithOne(x => x.Grupo)
                .HasForeignKey(x => x.IdGrupo);
            
            

        }
    }
}
