using BuscandoMiTrago.AccesoDatos.Repository;
using BuscandoMiTrago.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscandoMiTrago.AccesoDatos.Data
{
    public class BebidasRepository : Repository<proSolicitudBebidas>, IBebidasRepository
    {
        private readonly ApplicationDbContext _context;

        public BebidasRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
