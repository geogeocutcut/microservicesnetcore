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
    
    public class PartyRoleRepositoryInMemory:IPartyRoleRepository
    {
        private IStore<Guid> _store;
    	
    	public PartyRoleRepositoryInMemory(IStore<Guid> store)
    	{
    		_store=store;
    	}
    	
    	public async Task DeleteAsync(Guid id)
        {
            await _store.RemoveAsync<PartyRole>(id);
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

        public async Task<PartyRole> InsertAsync(PartyRole entity)
        {
            await _store.UpsertAsync<PartyRole>(entity);
            return entity;
        }
        public async Task<PartyRole> UpdateAsync(PartyRole entity)
        {
            await _store.UpsertAsync<PartyRole>(entity);
            return entity;
        }
    }
}
