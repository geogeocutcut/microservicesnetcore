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
    
    public class RoleEnumRepositoryInMemory:IRoleEnumRepository
    {
        private IStore<Guid> _store;
    	
    	public RoleEnumRepositoryInMemory(IStore<Guid> store)
    	{
    		_store=store;
    	}
    	
    	public async Task DeleteAsync(Guid id)
        {
            await _store.RemoveAsync<RoleEnum>(id);
        }

        public async Task<IList<RoleEnum>> GetAllAsync()
        {
            return await _store.GetAllAsync<RoleEnum>();
        }

        public async Task<RoleEnum> GetByIdAsync(Guid id)
        {
            return await _store.GetByIdAsync<RoleEnum>(id);
        }
        
        public async Task<IList<RoleEnum>> FindAsync(Expression<Func<RoleEnum, bool>> predicate)
        {
            return await _store.FindAsync<RoleEnum>(predicate);
        }

        public async Task<RoleEnum> InsertAsync(RoleEnum entity)
        {
            await _store.UpsertAsync<RoleEnum>(entity);
            return entity;
        }
        public async Task<RoleEnum> UpdateAsync(RoleEnum entity)
        {
            await _store.UpsertAsync<RoleEnum>(entity);
            return entity;
        }
    }
}
