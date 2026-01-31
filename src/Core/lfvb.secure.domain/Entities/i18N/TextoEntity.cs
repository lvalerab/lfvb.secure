using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.i18N
{
    public class TextoEntity
    {
        public Guid Id { get; set; } = Guid.Empty;

        public IList<TextoIdiomaEntity> TextosIdiomas { get; set; } = new List<TextoIdiomaEntity>();    
        public IList<ColumnaTextoIdiomaEntity> ColumnasTextosIdiomas { get; set; } = new List<ColumnaTextoIdiomaEntity>();
        public IList<VariableTextoEntity> Variables { get; set; } = new List<VariableTextoEntity>();    
        public IList<OpcionTextoEntity> Opciones { get; set; } = new List<OpcionTextoEntity>();
    }
}
