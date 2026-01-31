using AutoMapper;
using lfvb.secure.aplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Commands.QuitarGrupoPermisosUsuario
{
    public class QuitarGrupoPermisosUsuario: IQuitarGrupoPermisosUsuario
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public QuitarGrupoPermisosUsuario(IDataBaseService db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Execute(Guid idUsuario, List<Guid> grupos)
        {
            bool resultado = true;
            foreach (Guid grupo in grupos)
            {
                var relacion = _db.RelacionUsuariosGruposAplicaciones.FirstOrDefault(r => r.IdUsuario == idUsuario && r.IdGrupo == grupo);
                if (relacion != null)
                {
                    _db.RelacionUsuariosGruposAplicaciones.Remove(relacion);
                    resultado &= true;
                } else
                {
                    resultado = false; // Si no se encuentra la relación, marcamos el resultado como falso
                }
            }
            if (resultado)
            {
             await  _db.SaveAsync();
            }
            return resultado;
        }
    }
}
