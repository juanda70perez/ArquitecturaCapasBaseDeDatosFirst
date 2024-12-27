using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes);

        T Get(object[] keyValues);

        void Add(T tEntity);

        void Update(T entity);

        void Delete(T Tentity);

        void Save();

        Task<IEnumerable<T>> GetAsync(   Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes);

        Task<T> GetAsync(object[] keyValues);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T Tentity);

        Task SaveAsync();
    }
}
