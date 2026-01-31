using AutoMapper;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Credencial.Commands.CaducarCredencial
{
    public class CaducarCredencialCommand : ICaducarCredencialCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mp;
        private bool _transacion;

        public CaducarCredencialCommand(IDataBaseService db, IMapper mp, bool transacion=false)
        {
            _db = db;
            _mp = mp;
            _transacion = transacion;
        }

        public async Task<int> execute(Guid idUsuario, string codigoTipoCredencial)
        {
            int cuenta = 0;
            var credenciales = await (from c in _db.Credenciales
                                      where c.IdUsuario == idUsuario && c.CodigoTipoCredencial == codigoTipoCredencial && c.VigenteDesde<= DateTime.Now && (c.VigenteHasta== null || c.VigenteHasta>=DateTime.Now)
                                        select c).ToListAsync();
            foreach (var credencial in credenciales)
            {                
                credencial.VigenteHasta = DateTime.Now;
                _db.Credenciales.Update(credencial);
                cuenta++;
            }

            if(!_transacion)
            {
                await _db.SaveAsync();
            }

            return cuenta;
        }
    }
}
