using lfvb.secure.domain.Entities.Views.VWElemento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Configuraciones.Views
{
    public class VWElementoConfiguration
    {
        public VWElementoConfiguration(EntityTypeBuilder<VWElementoEntity> builder) {
            builder
                .ToView("view_reeu_relacion_elementos_usuarios");

            builder
                .Property(x => x.Id).HasColumnName("ID");
            builder
                .Property(x => x.Etiqueta).HasColumnName("ETIQUETA");
            builder
                .Property(x => x.Tipo).HasColumnName("TIPO");
            builder
                .Property(x=>x.IdUsuario).HasColumnName("ID_USUA");

        }
    }
}
