using BuscandoMiTrago.AccesoDatos.Repository;
using BuscandoMiTrago.Model;
using BuscandoMiTrago.Utilidades;
using Newtonsoft.Json;
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

        public ModelResponse EditarFavoritos(string id)
        {
            ModelResponse response = new();
            proSolicitudBebidas model = new proSolicitudBebidas();
            model.IdSolicitud = 1241;
            model.IdBebidas = id;
            var bebida = _context.proSolicitudBebidas.FirstOrDefault(x => x.IdBebidas == id);
            if (bebida == null)
            {            
                _context.proSolicitudBebidas.Add(model);
                _context.SaveChanges();
                response.Code = 1241;
                response.Comments = "Elemento agregado a favoritos";
                response.Response = JsonConvert.SerializeObject(model);
            }
            else
            {
                _context.proSolicitudBebidas.Remove(bebida);
                _context.SaveChanges();
                response.Code = 1241;
                response.Comments = "Elemento eliminado de favoritos";
                response.Response = JsonConvert.SerializeObject(model);
            }
            return response;
        }
    }
}
