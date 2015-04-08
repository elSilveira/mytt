using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mytt.Models
{
    public class Pesquisa
    {
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public bool Seguindo { get; set; }
    }
}