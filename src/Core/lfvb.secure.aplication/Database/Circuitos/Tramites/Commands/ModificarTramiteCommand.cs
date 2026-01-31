using AutoMapper;
using lfvb.secure.aplication.Database.Circuitos.Tramites.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Circuitos.Tramite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Tramites.Commands
{
    public class ModificarTramiteCommand : IModificarTramiteCommand
    {
        private readonly IDataBaseService _db;  
        private readonly IMapper _mp;   

        public ModificarTramiteCommand(IDataBaseService db, IMapper mp)
        {
            _db = db;
            _mp = mp;
        }

        public async Task<TramiteModel> execute(TramiteModel modelo)
        {
            if (modelo == null)
                throw new ArgumentNullException(nameof(modelo));

            if(modelo.Nombre.Trim() == "")
                throw new ArgumentException("El nombre del trámite no puede estar vacío.");

            if(modelo.Id == Guid.Empty)
                throw new ArgumentException("El Id del trámite no puede estar vacío.");

            //Buscamos el tramite a modificar

            TramiteEntity entidad=await (from tr in _db.Tramites
                                        where tr.Id==modelo.Id
                                        select tr).FirstOrDefaultAsync();

            if (entidad!=null)
            {
                entidad.Nombre=modelo.Nombre;
                entidad.Descripcion=modelo.Descripcion;
                entidad.Normativa=modelo.Normativa;
                _db.Tramites.Update(entidad);
                await _db.SaveAsync();
                return new TramiteModel
                {
                    Id = entidad.Id,
                    Nombre = entidad.Nombre,
                    Descripcion = entidad.Descripcion,
                    Normativa = entidad.Normativa
                };
            } else
            {
                return null;
            }
        }
    }
}
