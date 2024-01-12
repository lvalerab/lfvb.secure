using lfvb.secure.domain.Entities.Credencial;
using lfvb.secure.domain.Entities.PasswordCredencial;
using lfvb.secure.domain.Entities.TokenCredencial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones
{
    public class CredencialConfiguration
    {
        public CredencialConfiguration(EntityTypeBuilder<CredencialEntity> entityBuilder) {
            //Configuración de la tabla
            entityBuilder
                .ToTable("crus_credencial_usua")
                .HasKey(x => x.Id);

            entityBuilder
                .HasIndex(x => new { x.IdUsuario, x.CodigoTipoCredencial });                

            //Configuración de las columnas

            entityBuilder.Property(x => x.Id)
                .HasColumnName("ID_CRUS")
                .ValueGeneratedOnAdd();
            entityBuilder.Property(x => x.IdUsuario).HasColumnName("ID_USUA").IsRequired();
            entityBuilder.Property(x=>x.CodigoTipoCredencial).HasColumnName("COD_TRCR").IsRequired();
            entityBuilder.Property(x => x.VigenteDesde).HasColumnName("VIGENTE_DESDE_CRUS").IsRequired().HasDefaultValueSql("now()");
            entityBuilder.Property(x => x.VigenteHasta).HasColumnName("VIGENTE_HASTA_CRUS");

            //Relacion N a 1
            entityBuilder.HasOne(x => x.Usuario)
                         .WithMany(x => x.Credenciales)
                         .HasForeignKey(x => x.IdUsuario);

            entityBuilder.HasOne(x => x.TipoCredencial)
                         .WithMany(x => x.Credenciales)
                         .HasForeignKey(x => x.CodigoTipoCredencial);

            //Relancion 1 a 1
            entityBuilder.HasOne(x => x.Password)
                         .WithOne(x => x.Credencial)
                         .HasForeignKey<PasswordCredencialEntity>(x => x.Id)
                         .IsRequired(false);

            entityBuilder.HasOne(x=>x.Token)
                         .WithOne(x=>x.Credencial)
                         .HasForeignKey<TokenCredencialEntity>(x=>x.Id)
                         .IsRequired(false);

        }
    }
}
