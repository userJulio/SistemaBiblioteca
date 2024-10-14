using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRent.Repositories.Interface
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<ICollection<TEntity>> GetAsync();
        Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<ICollection<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy);
        Task<TEntity?> GetAsync(int id);
        Task AddAsync(TEntity entity);
        Task Grabar();
        Task DeleteAsync(int id);
    }
}
