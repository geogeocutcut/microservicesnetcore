using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Common.Model;
using MongoDB.Driver;

namespace Libragri.AuthenticationDomain.RepositoriesMongodb
{
    public class StoreMongodb<TEntity> where TEntity : BaseEntity<Guid>
    {
        private IMongoCollection<TEntity> _store;

        
        public StoreMongodb(IMongoDatabase db)
        {
            _store=db.GetCollection<TEntity>(typeof(TEntity).Name);
        }
        public async Task DeleteAsync(Guid id)
        {
            await _store.DeleteOneAsync(x=>x.Id==id);
        }

        public async Task<IList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await (await _store.FindAsync(predicate)).ToListAsync();
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await (await _store.FindAsync(Builders<TEntity>.Filter.Empty)).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await (await _store.FindAsync(x=>x.Id==id)).FirstOrDefaultAsync();
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            await _store.InsertOneAsync(entity);
            return await GetByIdAsync(entity.Id);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await _store.ReplaceOneAsync(
                    x => x.Id == entity.Id, 
                    entity, 
                    new UpdateOptions
                    {
                        IsUpsert = true
                    }
                );
            return entity;
        }
    }
}