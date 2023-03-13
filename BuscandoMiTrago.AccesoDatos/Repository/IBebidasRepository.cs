using BuscandoMiTrago.AccesoDatos.Models;
using BuscandoMiTrago.Model;
using BuscandoMiTrago.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscandoMiTrago.AccesoDatos.Repository
{
    public interface IBebidasRepository:IRepository<proSolicitudBebidas>
    {
        ModelResponse EditarFavoritos(string id);
        ModelResponse ObtenerBebidas();
    }
}
