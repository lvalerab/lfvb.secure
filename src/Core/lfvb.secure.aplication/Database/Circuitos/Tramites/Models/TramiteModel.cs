﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Tramites.Models
{
    public class TramiteModel
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }  
        public string Descripcion { get; set; } 
        public string Normativa { get; set; }   
    }
}
