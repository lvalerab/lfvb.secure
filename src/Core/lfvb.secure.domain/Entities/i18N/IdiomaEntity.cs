using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.i18N
{
    public class IdiomaEntity
    {
        public string Codigo { get; set; } = "";
        public Guid Id { get; set; } = Guid.Empty;
        public string Nombre { get; set; } = "";

        public IList<AgrupacionIdiomaEntity> Agrupaciones { get; set; } = new List<AgrupacionIdiomaEntity>();   
        public IList<AgrupacionIdiomaEntity> AgrupacionesPertenecientes { get; set; } = new List<AgrupacionIdiomaEntity>();

        public IList<TextoIdiomaEntity> Textos { get; set; } = new List<TextoIdiomaEntity>();
    }
}
