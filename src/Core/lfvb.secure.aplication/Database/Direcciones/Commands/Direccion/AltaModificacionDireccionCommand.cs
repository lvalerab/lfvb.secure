using lfvb.secure.aplication.Database.Direcciones.Models;
using lfvb.secure.aplication.Database.Elementos.Commands;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Direcciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Commands.Direccion
{
    public class AltaModificacionDireccionCommand: IAltaModificacionDireccionCommand
    {
        private readonly IDataBaseService _db;
        private readonly IAltaElementoCommand _altaElemCmd;

        public AltaModificacionDireccionCommand(IDataBaseService db, IAltaElementoCommand altaElemCmd)
        {
            _db = db;
            _altaElemCmd = altaElemCmd;
        }

        public async Task<DireccionModel> execute(DireccionModel model)
        {
            // Verificamos si es normalizada o no normalizada, solo puede ser de un tipo
            if (model.Normalizada != null && model.NoNormalizada != null)
            {
                throw new Exception("Una dirección no puede ser normalizada y no normalizada a la vez");
            }
            if (model.Normalizada == null && model.NoNormalizada == null)
            {
                throw new Exception("Una dirección debe ser normalizada o no normalizada");
            }
            if (model.Id == null)
            {
                // Si no tiene ID, es una nueva dirección   
                model.Id = await _altaElemCmd.execute("dire");
                // Crear una nueva entidad de dirección
                DireccionEntity diet = new DireccionEntity
                {
                    Id = model.Id.Value,
                };
                await _db.Direcciones.AddAsync(diet);
               
                if (model.Normalizada != null)
                {
                    DireccionNormalizadaEntity dinoet=new DireccionNormalizadaEntity {
                        Id = model.Id.Value,
                        IdCalle = model.Normalizada.Calle?.Id??throw new Exception("En la direccion normalizada, es necesario indicar una via o calle"),
                        Edificio = model.Normalizada.Edificio,
                        Numero = model.Normalizada.Numero,
                        Puerta = model.Normalizada.Puerta,
                        Piso = model.Normalizada.Piso,
                        Escalera = model.Normalizada.Escalera,
                        Bloque = model.Normalizada.Bloque,
                        Ampliacion = model.Normalizada.Ampliacion,
                        Direccion = diet
                    };
                    await _db.DireccionesNormalizadas.AddAsync(dinoet);
                } else
                {
                    if(model.NoNormalizada.Calle==null && model.NoNormalizada.Entidad==null)
                    {
                        throw new Exception("En las direcciones no normalizadas, o se debe indicar al menos la calle o la población");
                    }
                    DireccionNoNormalizadaEntity dinnet=new DireccionNoNormalizadaEntity {
                        Id = model.Id.Value,
                        IdCalle = model.NoNormalizada?.Calle?.Id,
                        IdEntidadTerritorial = model.NoNormalizada?.Entidad?.Id,
                        Linea1 = model.NoNormalizada?.Linea1??throw new Exception("En la direccion no normalizada, es necesario indicar al menos una linea de dirección"),
                        Linea2 = model.NoNormalizada?.Linea2,
                        Linea3 = model.NoNormalizada?.Linea3,
                        Direccion = diet
                    };
                    await _db.DireccionesNoNormalizadas.AddAsync(dinnet);   
                }
            }
            else
            {
                //La direccion ya existe, hay que modificarla   
                if(model.Normalizada != null)
                {
                    var existeDirNoNormalizada = await _db.DireccionesNoNormalizadas.FindAsync(model.Id.Value);
                    if (existeDirNoNormalizada != null)
                    {
                      _db.DireccionesNoNormalizadas.Remove(existeDirNoNormalizada);
                    }

                    DireccionNormalizadaEntity? dinoet = await _db.DireccionesNormalizadas.FindAsync(model.Id.Value);
                    if (dinoet == null)
                    {
                        throw new Exception("No se ha encontrado la dirección normalizada a modificar");
                    }
                    dinoet.IdCalle = model.Normalizada.Calle?.Id??throw new Exception("En la direccion normalizada, es necesario indicar una via o calle");
                    dinoet.Edificio = model.Normalizada.Edificio;
                    dinoet.Numero = model.Normalizada.Numero;
                    dinoet.Puerta = model.Normalizada.Puerta;
                    dinoet.Piso = model.Normalizada.Piso;
                    dinoet.Escalera = model.Normalizada.Escalera;
                    dinoet.Bloque = model.Normalizada.Bloque;
                    dinoet.Ampliacion = model.Normalizada.Ampliacion;
                } else
                {
                    var existeDirNormalizada = await _db.DireccionesNormalizadas.FindAsync(model.Id.Value);
                    if (existeDirNormalizada != null)
                    {
                      _db.DireccionesNormalizadas.Remove(existeDirNormalizada);
                    }

                    if (model.NoNormalizada.Calle==null && model.NoNormalizada.Entidad==null)
                    {
                        throw new Exception("En las direcciones no normalizadas, o se debe indicar al menos la calle o la población");
                    }
                    DireccionNoNormalizadaEntity? dinnet = await _db.DireccionesNoNormalizadas.FindAsync(model.Id.Value);
                    if (dinnet == null)
                    {
                        throw new Exception("No se ha encontrado la dirección no normalizada a modificar");
                    }
                    dinnet.IdCalle = model.NoNormalizada?.Calle?.Id;
                    dinnet.IdEntidadTerritorial = model.NoNormalizada?.Entidad?.Id;
                    dinnet.Linea1 = model.NoNormalizada?.Linea1??throw new Exception("En la direccion no normalizada, es necesario indicar al menos una linea de dirección");
                    dinnet.Linea2 = model.NoNormalizada?.Linea2;
                    dinnet.Linea3 = model.NoNormalizada?.Linea3;
                }
            }
            await _db.SaveAsync();
            return model;   
        }
    }
}
