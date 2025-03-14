using BuscandoMiTrago.AccesoDatos.Models;
using BuscandoMiTrago.AccesoDatos.Repository;
using BuscandoMiTrago.Model;
using BuscandoMiTrago.Utilidades;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BuscandoMiTrago.AccesoDatos.Data
{
    public class BebidasRepository : Repository<proSolicitudBebidas>, IBebidasRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public BebidasRepository(ApplicationDbContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _configuration = configuration;
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

        public ModelResponse ObtenerBebidas()
        {
            ModelResponse response = new();
            DrinksModel drinksList = new DrinksModel();
            try
            {
                var idBebidas = _context.proSolicitudBebidas.Where(x => x.IdSolicitud == 1241).OrderBy(x => x.Id).ToList();
                foreach (var item in idBebidas)
                {
                    DrinksModel bebida = new DrinksModel();
                    var request = RunRequest(_configuration.GetSection("GlobalParams:UrlObtenerDetalleBebida").Value + item.IdBebidas, "GET", null, null);

                    bebida = JsonConvert.DeserializeObject<DrinksModel>(request.Response);
                    drinksList.drinks.Add(bebida.drinks[0]);
                }

                response.Code = 1241;
                response.Comments = "Listado desde la base de datos";
                response.Response = JsonConvert.SerializeObject(drinksList);
                response.DrinksModel = drinksList;
            }
            catch (Exception ex)
            {
                response.Code = 1241;
                response.Comments = ex.Message;
                response.Response = JsonConvert.SerializeObject(drinksList);
            }          
            return response;
        }
    }
}
