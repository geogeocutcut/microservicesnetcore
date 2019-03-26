using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Core.Common.Store;
using Libragri.partyDomain.Model;
using Libragri.partyDomain.IRepositories;

namespace Libragri.partyDomain.Repositories
{
    
    public class RelationshipRepository:IRelationshipRepository
    {
        private IStore<Guid> _store;
    	
    	public RelationshipRepository(IStore<Guid> store)
    	{
    		_store=store;
    	}
    	
    	public async Task DeleteAsync(Relationship entity)
        {
            await _store.RemoveAsync<Relationship>(entity);
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

        public async Task<Relationship> UpsertAsync(Relationship entity)
        {
            await _store.UpsertAsync<Relationship>(entity);
            return entity;
        }
    }
}
