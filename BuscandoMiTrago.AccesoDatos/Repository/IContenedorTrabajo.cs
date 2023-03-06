using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscandoMiTrago.AccesoDatos.Repository
{
    public interface IContenedorTrabajo:IDisposable
    {
        public IBebidasRepository Bebida { get; }
        void Save();
    }
}
