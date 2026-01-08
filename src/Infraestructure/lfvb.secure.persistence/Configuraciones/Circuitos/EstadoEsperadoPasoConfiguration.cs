using lfvb.secure.domain.Entities.EstadoEsperadoPaso;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace lfvb.secure.persistence.Configuraciones.Circuitos
{
    public class EstadoEsperadoPasoConfiguration
    {
        public EstadoEsperadoPasoConfiguration(EntityTypeBuilder<EstadoEsperadoPasoEntity> builder)
        {
            builder
                .ToTable("epes_estado_esperado_paso")
                .HasKey(x => new {x.IdPaso, x.CodTipoElemento, x.CodEstado,x.TipoEstadoEsperado });

            builder.Property(x => x.IdPaso).HasColumnName("ID_PASO").IsRequired();
            builder.Property(x => x.CodTipoElemento).HasColumnName("COD_TIEL");
            builder.Property(x => x.CodEstado).HasColumnName("COD_ESTA").IsRequired();
            builder.Property(x => x.TipoEstadoEsperado).HasColumnName("TIPO_EPES").IsRequired();

            builder.HasOne(x => x.Paso).WithMany(p => p.EstadosEsperadosPaso).HasForeignKey(x => x.IdPaso);
            builder.HasOne(x=> x.TipoElemento).WithMany(t => t.EstadosEsperadosPaso).HasForeignKey(x => x.CodTipoElemento);
            builder.HasOne(x => x.Estado).WithMany(e => e.EstadosEsperadosPaso).HasForeignKey(x => x.CodEstado);
             
        }
    }
}
