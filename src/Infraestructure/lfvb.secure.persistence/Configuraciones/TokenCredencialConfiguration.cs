using lfvb.secure.domain.Entities.Credencial;
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
    public class TokenCredencialConfiguration
    {
        public TokenCredencialConfiguration(EntityTypeBuilder<TokenCredencialEntity> entityBuilder)
        {
            entityBuilder
                .ToTable("tknc_token_credencial")
                .HasKey(x => x.Id);
            entityBuilder.Property(x => x.Id).HasColumnName("ID_CRUS").IsRequired();
            entityBuilder.Property(x => x.Token).HasColumnName("TOKEN_TKNC").IsRequired();

            entityBuilder
                .HasOne(x => x.Credencial)
                .WithOne(x => x.Token)                
                .HasPrincipalKey<CredencialEntity>(x => x.Id).IsRequired();
                
        }
    }
}
