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
    
    public class RoleRepositoryInMemory:IRoleRepository
    {
        private IStore<Guid> _store;
    	
    	public RoleRepositoryInMemory(IStore<Guid> store)
    	{
    		_store=store;
    	}
    	
    	public async Task DeleteAsync(Guid id)
        {
            await _store.RemoveAsync<Role>(id);
        }

        public async Task<IList<Role>> GetAllAsync()
        {
            return await _store.GetAllAsync<Role>();
        }

        public async Task<Role> GetByIdAsync(Guid id)
        {
            return await _store.GetByIdAsync<Role>(id);
        }
        
        public async Task<IList<Role>> FindAsync(Expression<Func<Role, bool>> predicate)
        {
            return await _store.FindAsync<Role>(predicate);
        }

        public async Task<Role> InsertAsync(Role entity)
        {
            await _store.UpsertAsync<Role>(entity);
            return entity;
        }
        public async Task<Role> UpdateAsync(Role entity)
        {
            await _store.UpsertAsync<Role>(entity);
            return entity;
        }
    }
}
