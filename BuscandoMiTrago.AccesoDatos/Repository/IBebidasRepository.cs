using BuscandoMiTrago.Model;
using BuscandoMiTrago.Utilidades;

namespace BuscandoMiTrago.AccesoDatos.Repository
{
    public interface IBebidasRepository:IRepository<proSolicitudBebidas>
    {
        ModelResponse EditarFavoritos(string id);
        ModelResponse ObtenerBebidas();
    }
}
