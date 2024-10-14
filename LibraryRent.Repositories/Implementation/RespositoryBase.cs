using LibraryRent.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRent.Repositories.Implementation
{
    public abstract class RespositoryBase<TEntity>: IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly DbContext context;

        protected RespositoryBase(DbContext context)
        {
            this.context = context;
        }
        public  async Task<ICollection<TEntity>> GetAsync()
        {
            return await context.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>()
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TKey>> orderBy)
        {
            return await context.Set<TEntity>()
                .Where(predicate)
                .OrderBy(orderBy)
                .AsNoTracking()
                .ToListAsync();
        }

        public  async Task<TEntity?> GetAsync(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }
        public  async Task AddAsync(TEntity entity)
        {
            await context.Set<TEntity>()
                .AddAsync(entity);
            await Grabar();
        }

        public async Task Grabar()
        {
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
           var obtenerElemento= await GetAsync(id);
            if(obtenerElemento is not null)
            {
                context.Set<TEntity>().Remove(obtenerElemento);
                await Grabar();
            }

        }
    }
}
