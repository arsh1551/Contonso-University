using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
   
        public class Repository<T> : IRepository<T> where T : class
        {
            private IDbSet<T> _objectSet;

            public Repository(DbContext dbContext)
            {
                if (dbContext != null)
                {
                    _objectSet = dbContext.Set<T>();
                }
            }

            public IQueryable<T> AsQuerable()
            {
                return _objectSet.AsQueryable();
            }
            /// <summary>
            /// Gets a table
            /// </summary>
            public virtual IQueryable<T> Table
            {
                get
                {
                    return this._objectSet;
                }
            }

            public IEnumerable<T> GetAll()
            {
                return _objectSet.AsEnumerable();
            }

            public IEnumerable<T> GetAll(Func<T, bool> predicate)
            {
                return _objectSet.Where(predicate);
            }

            public T Get(Func<T, bool> predicate)
            {
                return _objectSet.Where(predicate).FirstOrDefault();
            }

            public void Add(T entity)
            {
                _objectSet.Add(entity);
            }

            public void Attach(T entity)
            {
                _objectSet.Attach(entity);
            }

            public void Delete(T entity)
            {
                _objectSet.Remove(entity);
            }
        
    }
}
