using DAL.Models;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class UnityOfWork:IDisposable
    {
        private DbContext _context;
        public Dictionary<Type, object> Repositories { get; private set; }
        public UnityOfWork(DbContext context)
        {
            Repositories = new Dictionary<Type, object>();
            _context = context;
        }

        private Dictionary<Type, List<LogProperty>> AuditInfo { get; set; }

        /// <summary>
        /// List of types that will be logged by the application
        /// </summary>
        private List<LogType> TypesToLog { get; set; }


        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();



        public IRepository<T> Repository<T>() where T : class
        {
            if (Repositories.Keys.Contains(typeof(T)) == true)
            {
                return Repositories[typeof(T)] as IRepository<T>;
            }
            IRepository<T> repo = new Repository<T>(_context);
            Repositories.Add(typeof(T), repo);
            return repo;
        }
        public void SaveChanges()
        {
           

            try
            {
                _context.SaveChanges();

            }
            catch
            {
                throw;
               
            }
            finally
            {
               
            }

        }


        public void SetModifiedState<T>(T t) where T : class
        {
            _context.Entry(t).State = EntityState.Modified;
        }

        private IEnumerable<LogProperty> FieldsToLog(Type entityType)
        {
            if (this.AuditInfo == null)
                this.AuditInfo = new Dictionary<Type, List<LogProperty>>();

            if (!this.AuditInfo.ContainsKey(entityType))
            {

                var properties = this.Repository<LogProperty>().GetAll(a => a.TypeKey == entityType.Name).ToList();

                this.AuditInfo.Add(entityType, properties);

                
            }

            return this.AuditInfo[entityType];
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();

                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }


}

