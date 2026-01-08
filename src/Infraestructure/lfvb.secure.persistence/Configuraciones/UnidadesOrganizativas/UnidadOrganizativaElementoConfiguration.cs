using lfvb.secure.domain.Entities.UnidadOrganizativaElemento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.UnidadesOrganizativas
{
    public class UnidadOrganizativaElementoConfiguration
    {
        public UnidadOrganizativaElementoConfiguration(EntityTypeBuilder<UnidadOrganizativaElementoEntity> builder)
        {
            builder
                .ToTable("unel_unor_elemento")
                .HasKey(uoe => new { uoe.CodUnor, uoe.IdElem });

            builder
                .Property(uoe => uoe.CodUnor).HasColumnName("COD_UNOR");
            builder
                .Property(uoe => uoe.IdElem).HasColumnName("ID_ELEM");

            //Relaciones
            builder.HasOne(uoe => uoe.UnidadOrganizativa)
                .WithMany(uo => uo.Elementos)
                .HasForeignKey(uoe => uoe.CodUnor);

            builder.HasOne(uoe => uoe.Elemento)
                .WithMany(e => e.UnidadesOrganizativas)
                .HasForeignKey(uoe => uoe.IdElem);
        }
    }
}
