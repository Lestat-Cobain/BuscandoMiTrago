
namespace BuscandoMiTrago.AccesoDatos.Repository
{
    public interface IContenedorTrabajo:IDisposable
    {
        public IBebidasRepository Bebida { get; }
        void Save();
    }
}
