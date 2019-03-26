using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.PartyDomain.Model;
using Libragri.PartyDomain.IRepositories;
using Core.Common.Store;

namespace Libragri.PartyDomain.Repositories
{
    
    public class PartyRepository:IPartyRepository
    {
        private IStore<Guid> _store;
    	
    	public PartyRepository(IStore<Guid> store)
    	{
    		_store=store;
    	}
    	
    	public async Task DeleteAsync(Party entity)
        {
            await _store.RemoveAsync<Party>(entity);
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

        public async Task<Party> UpsertAsync(Party entity)
        {
            await _store.UpsertAsync<Party>(entity);
            return entity;
        }
    }
}
