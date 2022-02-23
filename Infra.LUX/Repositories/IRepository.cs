using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public interface IRepository<T>
    {
        Task<IList<T>> GetAll(Expression<Func<T, bool>> predicate);
        Task<IList<T>> GetAll(string query);
        Task<IList<TResult>> GetAll<TResult>(string query);
        Task<T> GetByIdAsync(string id);
        Task<T> AddAsync(T entity);
        Task<bool> UpsertAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}
