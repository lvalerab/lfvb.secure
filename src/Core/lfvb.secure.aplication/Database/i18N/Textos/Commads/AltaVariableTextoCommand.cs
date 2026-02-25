using lfvb.secure.aplication.Database.Elementos.Commands;
using lfvb.secure.aplication.Database.i18N.Textos.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.i18N;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Textos.Commads
{
    public class AltaVariableTextoCommand: IAltaVariableTextoCommand
    {
        private readonly IDataBaseService _db;
        public readonly IAltaElementoCommand _cmdAltaElemento;

        public AltaVariableTextoCommand(IDataBaseService db, IAltaElementoCommand cmdAltaElemento)
        {
            _db = db;
            _cmdAltaElemento = cmdAltaElemento;
        }

        public async Task<VariableTextoModel> execute(VariableTextoModel variable)
        {
            if(variable.Id != null)
            {
                throw new Exception("El id de la variable debe ser nulo");
            }   
            if(variable.Texto == null || variable.Texto.Id==null)
            {
                throw new Exception("El texto de la variable no puede ser nulo");
            }
            if (variable.Variable == null)
            {
                throw new Exception("La variable no puede ser nula");
            }

            Guid id=await this._cmdAltaElemento.execute("vrtx");
            VariableTextoEntity variableEnt= new VariableTextoEntity()
            {
                Id = id,
                IdTexto = variable.Texto.Id??Guid.Empty,
                Variable = variable.Variable
            };  
            await this._db.VariablesTextos.AddAsync(variableEnt);
            await this._db.SaveAsync();
            variable.Id = id;
            return variable;
        }
    }
}
