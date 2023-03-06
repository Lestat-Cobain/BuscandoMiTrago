using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Security;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BuscandoMiTrago.AccesoDatos.Repository;
using BuscandoMiTrago.Utilidades;
using BuscandoMiTrago.AccesoDatos.Data;
using BuscandoMiTrago.Model;

namespace BuscandoMiTrago.AccesoDatos.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _contex;
        private readonly DbSet<T> dbSet;
        private readonly string _urlBase = "";

        public Repository(ApplicationDbContext context)
        {
            _contex = context;
            dbSet = context.Set<T>();

        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
                query = query.Where(filter);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }

            if (orderBy != null)
                return orderBy(query).ToList();

            return query;
        }

        public T GetFirtsOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
                query = query.Where(filter);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }

            return query.FirstOrDefault();
        }



        public void Remove(int id)
        {
            T entityToRemove = dbSet.Find(id);
            Remove(entityToRemove);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
        public ModelResponse RunRequest(string url, string method, string sessionId, string model = null)
        {
            var bit = new BitacoraService()
            {
                Url = url,
                Token=sessionId,
                FechaRequest=DateTime.Now,
                Body= model,
                Tipo=method,
            };

            ModelResponse response = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}");
                request.KeepAlive = false;
                request.Method = method;
                request.ContentType = "application/json;charset=UTF-8";
                request.AllowAutoRedirect = false;
                request.ServicePoint.Expect100Continue = false;
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AcceptAllCertifications);
                request.ProtocolVersion = HttpVersion.Version10;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
                if (!string.IsNullOrEmpty(sessionId))
                {
                    request.Headers.Add("Authorization", $"Bearer {sessionId}");
                }


                if (model != null)
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(model);
                    request.ContentLength = (long)bytes.Length;
                    Stream stream = request.GetRequestStream();
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Close();
                }

                using StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream());
                response = new ModelResponse { Code = 0, Comments = String.Empty, Response = sr.ReadToEnd() };
                bit.Header=request.Headers.ToString();
                bit.FechaResponse=DateTime.Now;
                bit.Response=response.Response;
                _contex.Add(bit);
                _contex.SaveChanges();

            }
            catch (Exception ex)
            {
                if (ex is WebException wex)
                {
                    using var stream = wex.Response.GetResponseStream();
                    using var sr = new StreamReader(stream);
                    response = new ModelResponse { Code = -1, Comments = ex.Message, Response = sr.ReadToEnd() };
                    bit.Response = response.Response;

                }
                else
                {
                    response = new ModelResponse { Code = -1, Comments = ex.Message, Response = String.Empty };
                }
                _contex.Add(bit);
                _contex.SaveChanges();
            }

            return response;
        }

        internal static bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
