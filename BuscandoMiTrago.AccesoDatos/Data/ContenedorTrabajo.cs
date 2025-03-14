using BuscandoMiTrago.AccesoDatos.Repository;
using Microsoft.Extensions.Configuration;

namespace BuscandoMiTrago.AccesoDatos.Data
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public ContenedorTrabajo(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            Bebida = new BebidasRepository(_context, _configuration);
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
