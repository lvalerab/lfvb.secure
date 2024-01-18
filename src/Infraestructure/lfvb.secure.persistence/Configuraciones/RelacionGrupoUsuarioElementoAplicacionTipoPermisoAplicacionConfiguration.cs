using lfvb.secure.domain.Entities.RelacionGrupoUsuarioElementoAplicacionTipoPermisoAplicacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones
{
    public class RelacionGrupoUsuarioElementoAplicacionTipoPermisoAplicacionConfiguration
    {
        public RelacionGrupoUsuarioElementoAplicacionTipoPermisoAplicacionConfiguration(EntityTypeBuilder<RelacionGrupoUsuarioElementoAplicacionTipoPermisoAplicacionEntity> builder)
        {
            builder
                .ToTable("GUEL_GUAP_ELAP")
                .HasKey(x => new { x.IdGrupo, x.IdElemento, x.CodigoTipoPermiso });

            builder.Property(x => x.IdGrupo).HasColumnName("ID_GUAP").IsRequired();
            builder.Property(x => x.IdElemento).HasColumnName("ID_ELAP").IsRequired();
            builder.Property(x => x.CodigoTipoPermiso).HasColumnName("COD_TPTE").IsRequired();

            builder.HasOne(x => x.Grupo)
                .WithMany(x => x.RelacionElementosPermisos)
                .HasForeignKey(x => x.IdGrupo);

            builder.HasOne(x => x.ElementoAplicacion)
                .WithMany(x => x.GruposPermisos)
                .HasForeignKey(x => x.IdElemento);

            builder.HasOne(x => x.TipoPermiso)
                .WithMany(x => x.RelacionElementosGrupos)
                .HasForeignKey(x => x.CodigoTipoPermiso);
        }
    }
}
