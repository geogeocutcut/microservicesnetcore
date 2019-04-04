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
    
    public class PartyRepositoryInMemory:IPartyRepository
    {
        private IStore<Guid> _store;
    	
    	public PartyRepositoryInMemory(IStore<Guid> store)
    	{
    		_store=store;
    	}
    	
    	public async Task DeleteAsync(Guid id)
        {
            await _store.RemoveAsync<Party>(id);
        }

        public async Task<IList<Party>> GetAllAsync()
        {
            return await _store.GetAllAsync<Party>();
        }

        public async Task<Party> GetByIdAsync(Guid id)
        {
            return await _store.GetByIdAsync<Party>(id);
        }
        
        public async Task<IList<Party>> FindAsync(Expression<Func<Party, bool>> predicate)
        {
            return await _store.FindAsync<Party>(predicate);
        }

        public async Task<Party> InsertAsync(Party entity)
        {
            await _store.UpsertAsync<Party>(entity);
            return entity;
        }
        public async Task<Party> UpdateAsync(Party entity)
        {
            await _store.UpsertAsync<Party>(entity);
            return entity;
        }
    }
}
