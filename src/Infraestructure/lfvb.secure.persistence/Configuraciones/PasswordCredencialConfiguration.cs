using lfvb.secure.domain.Entities.Credencial;
using lfvb.secure.domain.Entities.PasswordCredencial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones
{
    public class PasswordCredencialConfiguration
    {
        public PasswordCredencialConfiguration(EntityTypeBuilder<PasswordCredencialEntity> entityBuilder)
        {
            entityBuilder
                .ToTable("pwdc_password_credencial")
                .HasKey(x => x.Id);
            entityBuilder.Property(x => x.Id).HasColumnName("ID_CRUS");
            entityBuilder.Property(x => x.Password).HasColumnName("PASS_PWDC").IsRequired();

            entityBuilder
                .HasOne(x => x.Credencial)
                .WithOne(x => x.Password)                
                .HasPrincipalKey<CredencialEntity>(x => x.Id);
        }       
    }
}
