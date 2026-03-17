using lfvb.secure.domain.Entities.Direcciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Direcciones
{
    public class DireccionConfiguration
    {
        public DireccionConfiguration(EntityTypeBuilder<DireccionEntity> builder)
        {
            builder
                .ToTable("dire_direccion")
                .HasKey(x => x.Id); 

            builder.Property(x => x.Id).HasColumnName("ID_DIRE").IsRequired();

            builder.HasOne(x => x.DireccionNormalizada)
                .WithOne(dn=>dn.Direccion)
                .HasForeignKey<DireccionEntity>(x => x.Id);

            builder.HasOne(x => x.DireccionNoNormalizada)
                .WithOne(dnn=>dnn.Direccion)
                .HasForeignKey<DireccionEntity>(x => x.Id);
        }   
    }
}
