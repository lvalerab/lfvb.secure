using lfvb.secure.domain.Entities.UnidadOrganizativa;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.UnidadesOrganizativas
{
    public class UnidadOrganizativaConfiguration
    {
        public UnidadOrganizativaConfiguration(EntityTypeBuilder<UnidadOrganizativaEntity> builder)
        {
            builder
                .ToTable("unor_unidad_organizativa")
                .HasKey(u=>u.Codigo);

            builder
                .Property(u => u.Codigo).HasColumnName("COD_UNOR");
            builder
                .Property(u => u.CodTuno).HasColumnName("COD_TUNO");
            builder
                .Property(u => u.CodUnorPadre).HasColumnName("COD_UNOR_PADRE").IsRequired(false);
            builder
                .Property(u => u.Nombre).HasColumnName("NOMBRE_UNOR").HasMaxLength(255);
            builder
                .Property(u=> u.Descripcion).HasColumnName("DESCRIPCION_UNOR").IsRequired(false);
            

            //Relaciones
            builder.HasOne(u => u.TipoUnidadOrganizativa)
                .WithMany(tu => tu.UnidadesOrganizativas)
                .HasForeignKey(u => u.CodTuno);

            builder.HasOne(u => u.UnidadOrganizativaPadre)
                .WithMany(u => u.UnidadesOrganizativasHijas)
                .HasForeignKey(u => u.CodUnorPadre);

            builder.HasMany(u=>u.GruposUnidadesOrganizativas)
                .WithOne(gu=>gu.UnidadOrganizativa)
                .HasForeignKey(gu=>gu.CodUnor);

            builder.HasMany(u => u.GruposUnidadesOrganizativasRelacionadas)
                .WithOne(gu => gu.UnidadOrganizativaRelacionado)
                .HasForeignKey(gu => gu.CodUnorRelacionado);

            builder.HasMany(u => u.Elementos)
                .WithOne(uoe => uoe.UnidadOrganizativa)
                .HasForeignKey(uoe => uoe.CodUnor);

        }
    }
}
