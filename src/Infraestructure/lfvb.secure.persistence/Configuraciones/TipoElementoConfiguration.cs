using lfvb.secure.domain.Entities.TipoElemento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones
{
    public class TipoElementoConfiguration
    {
        public TipoElementoConfiguration(EntityTypeBuilder<TipoElementoEntity> entityBuilder) {
            entityBuilder
               .ToTable("tiel_tipo_elemento")
               .HasKey(x => x.Codigo);

            entityBuilder
                .Property(x => x.Codigo).HasColumnName("COD_TIEL").HasMaxLength(10);
            entityBuilder
                .Property(x => x.Nombre).HasColumnName("nombre_tiel").HasMaxLength(60);

            entityBuilder.HasMany(x => x.Elementos)
                .WithOne(x => x.TipoElemento)
                .HasForeignKey(x => x.CodigoTipoElemento);

            entityBuilder.HasMany(x => x.RelacionCircuitos)
                .WithOne(ct=>ct.TipoElemento)
                .HasForeignKey(ct=>ct.CodigoTipoElemento);

            entityBuilder.HasMany(x=>x.Acciones)
                .WithOne(a=>a.TipoElemento)
                .HasForeignKey(a=>a.CodigoTipoElemento);

            entityBuilder.HasMany(x => x.EstadosEsperadosPaso)
                .WithOne(ep => ep.TipoElemento)
                .HasForeignKey(x => x.CodTipoElemento);    

        }
    }
}
