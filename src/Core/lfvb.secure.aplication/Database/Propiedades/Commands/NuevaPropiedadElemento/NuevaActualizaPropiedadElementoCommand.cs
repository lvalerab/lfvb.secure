using AutoMapper;
using lfvb.secure.aplication.Database.Propiedades.Queries.GetPropiedadesElemento;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.PropiedadElemento;
using lfvb.secure.domain.Entities.ValorPropiedadElemento;
using Microsoft.EntityFrameworkCore;


namespace lfvb.secure.aplication.Database.Propiedades.Commands.NuevaPropiedadElemento
{
    public class NuevaActualizaPropiedadElementoCommand : INuevaActualizaPropiedadElementoCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public NuevaActualizaPropiedadElementoCommand(IDataBaseService db, IMapper mp)
        {
            this._db = db;
            this._mapper = mp;
        }

        public async Task<PropiedadElementoModel> Execute(PropiedadElementoModel propiedad)
        {
            PropiedadElementoEntity entity = null;
            //Comprobamos si la propiedad es nueva o no, (con el ID a null sabemos que es nueva)
            if (propiedad.Id == null)
            {
                //Si es nueva, comprobamos que la propiedad no exista ya para el elemento
                List<PropiedadElementoEntity> propiedades = await this._db.PropiedadesElementos.Where(x => x.IdElemento == propiedad.IdElemento && x.CodPropiedad.Equals(propiedad.Propiedad.Codigo)).ToListAsync<PropiedadElementoEntity>();
                if (propiedades != null && propiedades.Count > 0)
                {
                    //Las propiedades anteriores, las desactivamos
                    foreach (var prop in propiedades)
                    {
                        prop.Activo = "N";
                        this._db.PropiedadesElementos.Update(prop);
                    }
                }
                //Creamos la nueva propiedad
                entity = new PropiedadElementoEntity
                {
                    IdElemento = propiedad.IdElemento,
                    Activo = "S",
                    FechaValor = DateTime.Now,
                    CodPropiedad = propiedad.Propiedad.Codigo,
                    Valores = new List<ValorPropiedadElementoEntity>()
                };
            } else
            {
                //Buscamos la propiedad por ID, si no existe, lanzamos una excepción
                entity = await this._db.PropiedadesElementos.Include(p=>p.Valores).Where(x => x.Id == propiedad.Id).FirstOrDefaultAsync<PropiedadElementoEntity>();
                if(entity == null)
                {
                    throw new Exception("No se ha encontrado la propiedad con el ID indicado");
                } else
                {
                    //Depende del tipo de propiedad, hacemos una cosa u otra
                    if(propiedad.Propiedad.TipoPropiedad.Historico)
                    {
                        //Si es historico, desactivamos el resto de propiedades y siempre damos de alta una nueva propiedad //aunque tenga el ID
                        List<PropiedadElementoEntity> propiedades = await this._db.PropiedadesElementos.Include(p=>p.Valores).Where(x => x.IdElemento == propiedad.IdElemento && x.CodPropiedad.Equals(propiedad.Propiedad.Codigo)).ToListAsync<PropiedadElementoEntity>();
                        foreach (var prop in propiedades)
                        {
                            prop.Activo = "N";
                            this._db.PropiedadesElementos.Update(prop);
                        }
                        //Creamos una nueva propiedad
                        entity = new PropiedadElementoEntity
                        {
                            IdElemento = propiedad.IdElemento,
                            Activo = "S",
                            FechaValor = DateTime.Now,
                            CodPropiedad = propiedad.Propiedad.Codigo,
                            Valores = new List<ValorPropiedadElementoEntity>()
                        };
                    }
                    else
                    {
                        //Si no es historico, actualizamos la propiedad existente
                        entity.Activo = "S";
                        entity.FechaValor = DateTime.Now;
                    }
                }
            }
            //Agregamos los valores de la prorpiedad, si existen
            if(propiedad.Valores != null && propiedad.Valores.Count > 0)
            {
                foreach (var valor in propiedad.Valores)
                {
                    ValorPropiedadElementoEntity encontrado = null;
                    if(valor.Id != null && valor.Id > 0)
                    {
                        //Si el valor tiene ID, lo buscamos en la entidad
                        encontrado = entity.Valores.FirstOrDefault(x => x.Id == valor.Id);
                    }                    
                    if (encontrado != null)
                    {
                        encontrado.Numerico = valor.Numero;
                        encontrado.NumericoMaximo = valor.NumeroMaximo;
                        encontrado.Fecha = valor.Fecha;
                        encontrado.FechaMaximo = valor.FechaMaxima;
                        encontrado.Texto = valor.Texto;
                        encontrado.Booleano = valor.Bool ?? false ? "S" : "N";
                    }
                    else {
                        ValorPropiedadElementoEntity nuevov = new ValorPropiedadElementoEntity
                        {
                            IdPropiedadElemento = entity.Id,
                            Numerico = valor.Numero,
                            NumericoMaximo = valor.NumeroMaximo,
                            Fecha = valor.Fecha,
                            FechaMaximo = valor.FechaMaxima,
                            Texto = valor.Texto,
                            Booleano = valor.Bool ?? false ? "S" : "N"
                        };
                        entity.Valores.Add(nuevov);
                    }
                }
            }
            if(entity.Id == 0)
            {
                //Si es una propiedad nueva, la añadimos a la base de datos
                await this._db.PropiedadesElementos.AddAsync(entity);
            } else
            {
                //Si es una propiedad existente, la actualizamos
                this._db.PropiedadesElementos.Update(entity);
            }
            await this._db.SaveAsync();
            propiedad.Id = entity.Id;
            return propiedad;           
        }
    }
}
