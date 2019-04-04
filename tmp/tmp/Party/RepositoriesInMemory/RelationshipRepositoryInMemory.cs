using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Core.Repository;
using Libragri.partyDomain.Model;
using Libragri.partyDomain.IRepositories;

namespace Libragri.partyDomain.RepositoriesInMemory
{
    
    public class RelationshipRepositoryInMemory:IRelationshipRepository
    {
        private IStore<Guid> _store;
    	
    	public RelationshipRepositoryInMemory(IStore<Guid> store)
    	{
    		_store=store;
    	}
    	
    	public async Task DeleteAsync(Guid id)
        {
            await _store.RemoveAsync<Relationship>(id);
        }

        public async Task<IList<Relationship>> GetAllAsync()
        {
            return await _store.GetAllAsync<Relationship>();
        }

        public async Task<Relationship> GetByIdAsync(Guid id)
        {
            return await _store.GetByIdAsync<Relationship>(id);
        }
        
        public async Task<IList<Relationship>> FindAsync(Expression<Func<Relationship, bool>> predicate)
        {
            return await _store.FindAsync<Relationship>(predicate);
        }

        public async Task<Relationship> InsertAsync(Relationship entity)
        {
            await _store.UpsertAsync<Relationship>(entity);
            return entity;
        }
        public async Task<Relationship> UpdateAsync(Relationship entity)
        {
            await _store.UpsertAsync<Relationship>(entity);
            return entity;
        }
    }
}
