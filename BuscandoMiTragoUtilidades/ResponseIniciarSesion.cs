using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscandoMiTrago.Utilidades
{
    public class ResponseIniciarSesion
    {
        public string? Token { get; set; }
        public int idUsuario { get; set; }

        public int? idPerfil { get; set; }

        public int status { get; set; }

        public string? message { get; set; }

        public string? Nombre { get; set; }

        public string? correo { get; set; }

        public string? usuario { get; set; }

    }
}
