using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Common.Model;

namespace Core.Repository
{
    public class StoreInMemory<TId> : IStore<TId>
    {
        private IDictionary<Type,IDictionary<TId,object>> data = new Dictionary<Type,IDictionary<TId,object>>();

        public async Task<IList<TEntity>> GetAllAsync<TEntity>() where TEntity:Entity<TId>
        {
            return await Task.Run(()=>{
                data.TryGetValue(typeof(TEntity),out var dataEntity);
                if(dataEntity==null)
                {
                    return null;
                }
                return ((ICollection<TEntity>)dataEntity.Values).ToList();
            });
        }

        public async Task<TEntity> GetByIdAsync<TEntity>(TId id) where TEntity:Entity<TId>
        { 
            return await Task.Run(()=>{
                data.TryGetValue(typeof(TEntity),out var dataEntity);
                if(dataEntity==null)
                {
                    return default(TEntity);
                }
                dataEntity.TryGetValue(id,out object entity);
                return (TEntity)entity;
            });
        }

        public async Task<IList<TEntity>> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity:Entity<TId>
        {
            return await Task.Run(()=>{
                data.TryGetValue(typeof(TEntity),out var dataEntity);
                return dataEntity?.Select(x=>(TEntity)x.Value)?.AsQueryable().Where(predicate)?.ToList();
            });
        }

        public async Task RemoveAsync<TEntity>(TEntity entity) where TEntity:Entity<TId>
        {
            await Task.Run(()=>{
                data.TryGetValue(typeof(TEntity),out var dataEntity);
                dataEntity?.Remove(entity.GetId());
            });
        }

        public async Task<TEntity> UpsertAsync<TEntity>(TEntity entity) where TEntity:Entity<TId>
        {
            return await Task.Run(()=>{
                data.TryGetValue(typeof(TEntity),out var dataEntity);
                if(dataEntity==null)
                {
                    data[typeof(TEntity)]= new Dictionary<TId,object>();
                }
                data[typeof(TEntity)][entity.GetId()]=entity;

                return entity;
            });
        }
    }
}
