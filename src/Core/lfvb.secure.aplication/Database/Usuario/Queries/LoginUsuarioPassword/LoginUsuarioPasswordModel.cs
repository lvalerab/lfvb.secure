﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Queries.LoginUsuarioPassword
{
    public class LoginUsuarioPasswordModel
    {
        public Guid? Id { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
    }
}
