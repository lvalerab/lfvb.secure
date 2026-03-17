using lfvb.secure.domain.Entities.NucleoSistema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.NucleoSistema
{
    public class NucleoSistemaConfiguration
    {
        public NucleoSistemaConfiguration(EntityTypeBuilder<NucleoSistemaEntity> builder)
        {
            builder.ToTable("nusi_nucleo_sistema")
                     .HasKey(ns=>ns.Codigo);

            builder.Property(ns => ns.Codigo).HasColumnName("COD_TIEL").HasDefaultValue("core");
            builder.Property(ns => ns.IdNucleo).HasColumnName("ID_NUSI");

            builder.HasOne(ns => ns.Elemento)
                   .WithMany(e => e.Nucleos)
                   .HasForeignKey(e=>e.IdNucleo);   
        }
    }
}
