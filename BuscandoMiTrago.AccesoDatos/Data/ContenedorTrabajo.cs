using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuscandoMiTrago.AccesoDatos.Repository;

namespace BuscandoMiTrago.AccesoDatos.Data
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly ApplicationDbContext _context;

        public ContenedorTrabajo(ApplicationDbContext context)
        {
           _context = context;
            Bebida = new BebidasRepository(_context);
        }

        public IBebidasRepository Bebida { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
