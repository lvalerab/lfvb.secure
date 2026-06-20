using lfvb.secure.domain.Entities.Calendario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Calendario
{
    public class EntradaCalendarioConfiguration
    {
        public EntradaCalendarioConfiguration(EntityTypeBuilder<EntradaCalendarioEntity> builder)
        {
            builder
                .ToTable("encl_entrada_calendario")
                .HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID_ENCL").IsRequired();
            builder.Property(x => x.IdTipoEntradaCalendario).HasColumnName("ID_TENC").IsRequired();
            builder.Property(x => x.IdUsuarioCreador).HasColumnName("ID_USUA_CREADOR").IsRequired();
            builder.Property(x => x.Titulo).HasColumnName("TITULO_ENCL").IsRequired().HasMaxLength(60);
            builder.Property(x => x.Descripcion).HasColumnName("DESCRIPCION_ENTR").IsRequired();
            builder.Property(x => x.FechaInicio).HasColumnName("FECHA_INICIO_ENCL").IsRequired();
            builder.Property(x => x.FechaFin).HasColumnName("FECHA_FIN_ENCL").IsRequired();
            //Relaciones
            builder.HasOne(x => x.TipoEntrada)
                .WithMany(x => x.EntradasCalendario)
                .HasForeignKey(x => x.IdTipoEntradaCalendario);

             builder.HasOne(x => x.UsuarioCreador)
                .WithMany(x => x.EntradasCalendario)
                .HasForeignKey(x => x.IdUsuarioCreador);

            builder.HasMany(x => x.Calendarios)
                .WithOne(x => x.EntradaCalendario)
                .HasForeignKey(x => x.IdEntradaCalendario);

            builder.HasMany(x => x.Participantes)
                .WithOne(x => x.EntradaCalendario)
                .HasForeignKey(x => x.IdEntradaCalendario);

            builder.HasMany(x => x.Elementos)
                .WithOne(x => x.EntradaCalendario)
                .HasForeignKey(x => x.IdEntradaCalendario);
        }
    }
}
