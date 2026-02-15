using lfvb.secure.aplication.Database.i18N.Textos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Composiciones.Models
{
    public class OpcionCampoColeccionTextoModel
    {

        public Guid? Id { get; set; }        
        public CampoColeccionTextoModel? Campo { get; set; }  
        public TextoModel? Texto { get; set; }
    }
}
