using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BuscandoMiTrago.Utilidades;

namespace BuscandoMiTrago.AccesoDatos.Repository
{
    public interface IRepository<T> where T : class
    {
        T? Get(int id);

        IEnumerable<T> GetAll(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeProperties = null
            );

        T? GetFirtsOrDefault(
            Expression<Func<T, bool>>? filter = null,
            string? includeProperties = null);

        void Add(T entity);
        void Remove(int id);
        void Remove(T entity);
        ModelResponse RunRequest(string url, string method, string sessionId, string model = null);
    }
}
