using lfvb.secure.domain.Entities.Circuitos.Circuito;
using lfvb.secure.persistence.Conversions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Circuitos
{
    public class CircuitoConfiguration
    {
        public CircuitoConfiguration(EntityTypeBuilder<CircuitoEntity> builder)
        {
            builder
                .ToTable("CIRC_CIRCUITO")
                .HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID_CIRC").IsRequired().HasConversion(v => GuidConversion.toString(v), v => GuidConversion.toGuid(v));
            builder.Property(x => x.IdTramite).HasColumnName("ID_TRAM").IsRequired();
            builder.Property(x => x.Nombre).HasColumnName("NOMBRE_CIRC").IsRequired();
            builder.Property(x => x.Descripcion).HasColumnName("DESCRIPCION_CIRC");
            builder.Property(x => x.Normativa).HasColumnName("NORMATIVA_CIRC");           
            builder.Property(x => x.Activo).HasColumnName("ACTIVO_CIRC").IsRequired();
            builder.Property(x => x.FechaAlta).HasColumnName("FECHA_ALTA").IsRequired();
            builder.Property(x => x.FechaModificacion).HasColumnName("FECHA_MODIFICACION").IsRequired();
            builder.Property(x => x.FechaBaja).HasColumnName("FECHA_BAJA");


            //Relacion 1 a muchos
            builder.HasOne(c => c.Tramite)
                .WithMany(t => t.Circuitos)
                .HasForeignKey(t => t.IdTramite);

            //Relaciones muchos a 1
            builder.HasMany(c=>c.RelacionTiposElementos)
                .WithOne(tc => tc.Circuito)
                .HasForeignKey(tc => tc.IdCircuito);  

            builder.HasMany(c => c.GruposAdministradores)   
                .WithOne(ga => ga.Circuito)
                .HasForeignKey(ga => ga.IdCircuito);

            builder.HasMany(c=>c.Pasos)
                .WithOne(p => p.Circuito)
                .HasForeignKey(p => p.IdCircuito);

            builder.HasMany(c=>c.PasosSiguientes)
                .WithOne(p => p.CircuitoSiguiente)
                .HasForeignKey(p => p.IdCircuitoSiguiente);

            builder.HasMany(c => c.ElementosEstados)
                .WithOne(ee => ee.Circuito)
                .HasForeignKey(ee => ee.IdCircuito);

            builder.HasMany(c => c.PasosErrores)
                .WithOne(pe => pe.CircuitoError)
                .HasForeignKey(c => c.IdCircuitoError);
        }
    }
}
