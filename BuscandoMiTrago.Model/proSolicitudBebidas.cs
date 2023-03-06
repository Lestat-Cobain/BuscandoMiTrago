using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscandoMiTrago.Model
{
    [Table("proSolicitudBebidas")]
    public class proSolicitudBebidas
    {
        [Key]
        public int Id { get; set; }
        public int IdSolicitud { get; set; }
        public string? IdBebidas { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
