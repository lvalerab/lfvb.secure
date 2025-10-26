using AutoMapper;
using lfvb.secure.aplication.Database.Circuitos.Tramites.Models;
using lfvb.secure.aplication.Database.Elementos.Commands;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Circuitos.Tramite;
using lfvb.secure.domain.Entities.Elemento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Tramites.Commands
{
    public class AltaTramiteCommand : IAltaTramiteCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mp;
        private readonly IAltaElementoCommand _altaElementoCommand;

        public AltaTramiteCommand(IDataBaseService db, IMapper mp, IAltaElementoCommand cmAltaElemento)
        {
            _db = db;
            _mp = mp;
            _altaElementoCommand = cmAltaElemento;
        }

        public async Task<TramiteModel> execute(AltaTramiteModel modelo)
        {
            if(modelo == null)
                throw new ArgumentNullException(nameof(modelo));

            if(modelo.Nombre.Trim()=="")
                throw new ArgumentException("El nombre del trámite no puede estar vacío.");

            //Damos de alta el elemento
            Guid id=await _altaElementoCommand.execute("tram");    

            TramiteEntity entidad = new TramiteEntity            
            {
                Id = id,
                Nombre = modelo.Nombre,
                Descripcion = modelo.Descripcion,
                Normativa = modelo.Normativa
            };

            await _db.Tramites.AddAsync(entidad);
            await _db.SaveAsync();

            return new TramiteModel
            {
                Id = entidad.Id,
                Nombre = entidad.Nombre,
                Descripcion = entidad.Descripcion,
                Normativa = entidad.Normativa
            };
        }
    }
}
