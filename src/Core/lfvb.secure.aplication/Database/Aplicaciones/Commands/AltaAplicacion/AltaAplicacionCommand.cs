using AutoMapper;
using lfvb.secure.aplication.Database.Aplicaciones.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Aplicacion;
using lfvb.secure.domain.Entities.Elemento;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Commands.AltaAplicacion
{
    public class AltaAplicacionCommand: IAltaAplicacionCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public AltaAplicacionCommand(IDataBaseService db, IMapper mp)
        {
            this._db = db;
            this._mapper = mp;
        }


        public async Task<AplicacionModel> Execute(AltaAplicacionModel model)
        {
            if(model == null || model.Codigo==null || model.Nombre==null)
            {
                throw new ArgumentNullException(nameof(model), "El modelo de aplicación no puede ser nulo.");
            } else
            {
                //Comprobamos si ya existe una aplicación con el mismo código
                var app = this._db.Aplicaciones.Where(x => x.Codigo.Equals(model.Codigo)).FirstOrDefault();
                if(app != null)
                {
                    throw new Exception("Ya existe una aplicación con el código indicado");
                } else
                {
                    //Creamos la nueva aplicación
                    app = new AplicacionEntity
                    {
                        Codigo = model.Codigo,
                        Nombre = model.Nombre
                    };
                    this._db.Aplicaciones.Add(app);

                    //Guardamos el elemento
                    ElementoEntity elem = new ElementoEntity
                    {
                        Id = app.Id,
                        CodigoTipoElemento = "apli"
                    };
                    this._db.Elementos.Add(elem);

                    await this._db.SaveAsync();

                    AplicacionModel devolver=await (from a in this._db.Aplicaciones
                                                    where a.Id == app.Id
                                                    select new AplicacionModel
                                                    {
                                                        Id = a.Id,
                                                        Codigo = a.Codigo,
                                                        Nombre = a.Nombre
                                                    }).FirstOrDefaultAsync<AplicacionModel>();   
                    return devolver;
                }
            }
        }
    }    
}
