using lfvb.secure.domain.Entities.Circuitos.Circuito;
using lfvb.secure.domain.Entities.Circuitos.Estado;
using lfvb.secure.domain.Entities.Circuitos.EstadoElementoSiguiente;
using lfvb.secure.domain.Entities.Elemento;
using lfvb.secure.domain.Entities.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Circuitos.EstadoElemento
{
    public class EstadoElementoEntity
    {
        public long Id { get; set; }
        public Guid IdElemento { get; set; }    
        public DateTime Fecha { get; set; }
        public Guid IdUsuarioTramitador { get; set; }   
        public string CodEstado { get; set; }
        public Guid IdCircuito { get; set; }

        public ElementoEntity Elemento { get; set; }
        public UsuarioEntity UsuarioTramitador { get; set; }
        public EstadoEntity Estado { get; set; }    
        public CircuitoEntity Circuito { get; set; }

        public EstadoElementoSiguienteEntity EstadoSiguiente { get; set; }
        public EstadoElementoSiguienteEntity EstadoAnterior { get; set; }
    }
}
