

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Common.Model;

namespace Core.Repository
{
    public interface IStore<TId>
    {
        
        Task<TEntity> GetByIdAsync<TEntity>(TId id) where TEntity : Entity<TId>;
        Task<TEntity> UpsertAsync<TEntity>(TEntity entity) where TEntity : Entity<TId>;
        Task RemoveAsync<TEntity>(TId id) where TEntity : Entity<TId>;
        Task<IList<TEntity>> GetAllAsync<TEntity>() where TEntity : Entity<TId>;
        Task<IList<TEntity>> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity<TId>;


    }
}
