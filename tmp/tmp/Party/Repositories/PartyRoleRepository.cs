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
    
    public class PartyRoleRepository:IPartyRoleRepository
    {
        private IStore<Guid> _store;
    	
    	public PartyRoleRepository(IStore<Guid> store)
    	{
    		_store=store;
    	}
    	
    	public async Task DeleteAsync(PartyRole entity)
        {
            await _store.RemoveAsync<PartyRole>(entity);
        }

        public async Task<IList<PartyRole>> GetAllAsync()
        {
            return await _store.GetAllAsync<PartyRole>();
        }

        public async Task<PartyRole> GetByIdAsync(Guid id)
        {
            return await _store.GetByIdAsync<PartyRole>(id);
        }
        
        public async Task<IList<PartyRole>> FindAsync(Expression<Func<PartyRole, bool>> predicate)
        {
            return await _store.FindAsync<PartyRole>(predicate);
        }

        public async Task<PartyRole> UpsertAsync(PartyRole entity)
        {
            await _store.UpsertAsync<PartyRole>(entity);
            return entity;
        }
    }
}
