using BuscandoMiTrago.Models;
using BuscandoMiTrago.AccesoDatos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BuscandoMiTrago.AccesoDatos.Repository;
using Org.BouncyCastle.Crypto;
using AutoMapper;
using Newtonsoft.Json;
using BuscandoMiTrago.Model;
using System.Linq;
using BuscandoMiTrago.Utilidades;
using BuscandoMiTrago.AccesoDatos.Models;

namespace BuscandoMiTrago.Controllers
{
    public class HomeController : Controller
    {
        #region Campos

        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IContenedorTrabajo _contenedorTrabajo;

        #endregion

        #region Constructores

        /// <summary>
        /// Inyección de dependencias
        /// </summary>
        /// <param name="configuration">Configuración de la aplicación</param>
        /// <param name="contenedorTrabajo">Configuración de acceso a datos</param>
        /// <param name="mapper">Datos de directorios de la aplicación</param>
        public HomeController(IContenedorTrabajo contenedorTrabajo, IMapper mapper, IConfiguration configuration)
        {
            _configuration = configuration;
            _contenedorTrabajo = contenedorTrabajo;
            _mapper = mapper;        
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult>GetBebidas(string bebida)
        {
            DrinksModel? drinksList;
            var requestByName = _contenedorTrabajo.Bebida.RunRequest(_configuration.GetSection("GlobalParams:UrlBusquedaPorNombre").Value + bebida, "GET", null, null);
            drinksList = JsonConvert.DeserializeObject<DrinksModel>(requestByName.Response); 
            if (drinksList.drinks == null)
            {
                var requestByIngredient = _contenedorTrabajo.Bebida.RunRequest(_configuration.GetSection("GlobalParams:UrlBusquedaPorIngrediente").Value + bebida, "GET", null, null);
                drinksList = JsonConvert.DeserializeObject<DrinksModel>(requestByIngredient.Response);
            }
            
            return Json(drinksList);
        }

        [HttpGet]
        public async Task<IActionResult> GetDetalleBebida(string id)
        {
            DrinksModel? drinkDetail;
            var request = _contenedorTrabajo.Bebida.RunRequest(_configuration.GetSection("GlobalParams:UrlObtenerDetalleBebida").Value + id, "GET", null, null);
            drinkDetail = JsonConvert.DeserializeObject<DrinksModel>(request.Response);
            return Ok(drinkDetail);
        }

        [HttpPost]
        public async Task<IActionResult> ToFavorites(string id)
        {
            ModelResponse response = _contenedorTrabajo.Bebida.EditarFavoritos(id);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetFavorites(int id)
        {
            ModelResponse response = _contenedorTrabajo.Bebida.ObtenerBebidas();
            return Ok(response);
        }
    }
}