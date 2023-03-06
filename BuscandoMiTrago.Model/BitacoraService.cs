using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscandoMiTrago.Model
{
    [Table("bitPeticionesServicios")]
    public class BitacoraService
    {
        [Key]
        public int IdPeticion { get; set; }
        public int IdUsuario { get; set; }
        public int? IdDatosBasicos { get; set; }
        public string Url { get; set; }
        public string Recurso { get; set; }
        public string Tipo { get; set; }
        public string Credentials { get; set; }
        public string Token { get; set; }
        public string Header { get; set; }
        public string Parameters { get; set; }
        public string Body { get; set; }
        public string Response { get; set; }
        public DateTime FechaRequest { get; set; }
        public DateTime? FechaResponse { get; set; }
    }
}
