﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Queries.GetAllUsuarios
{
    public interface IGetAllUsuariosQuery
    {
        public Task<List<GetAllUsuariosModel>> Execute();
    }
}
