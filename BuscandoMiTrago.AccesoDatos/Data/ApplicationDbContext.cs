using BuscandoMiTrago.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscandoMiTrago.AccesoDatos.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public virtual DbSet<proSolicitudBebidas> proSolicitudBebidas { get; set; }
        public virtual DbSet<BitacoraService> BitacoraServices { get; set; }
    }
}
