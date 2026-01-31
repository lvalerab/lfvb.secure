using lfvb.secure.domain.Entities.GrupoUnidadOrganizativa;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.UnidadesOrganizativas
{
    public class GrupoUnidadOrganizativaConfiguration
    {
        public GrupoUnidadOrganizativaConfiguration(EntityTypeBuilder<GrupoUnidadOrganizativaEntity> builder)
        {
            builder
                .ToTable("gruo_grupo_unor")
                .HasKey(guo => new { guo.CodUnor, guo.CodUnorRelacionado });

            builder
                .Property(guo => guo.CodUnor).HasColumnName("COD_UNOR");
            builder
                .Property(guo => guo.CodUnorRelacionado).HasColumnName("COD_UNOR_RELACIONADO");

            //Relaciones
            builder.HasOne(guo => guo.UnidadOrganizativa)
                .WithMany(uo => uo.GruposUnidadesOrganizativas)
                .HasForeignKey(guo => guo.CodUnor);

            builder.HasOne(guo => guo.UnidadOrganizativaRelacionado)
                .WithMany(uo => uo.GruposUnidadesOrganizativasRelacionadas)
                .HasForeignKey(guo => guo.CodUnorRelacionado);
        }
    }
}
