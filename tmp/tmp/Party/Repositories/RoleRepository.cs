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
    
    public class RoleRepository:IRoleRepository
    {
        private IStore<Guid> _store;
    	
    	public RoleRepository(IStore<Guid> store)
    	{
    		_store=store;
    	}
    	
    	public async Task DeleteAsync(Role entity)
        {
            await _store.RemoveAsync<Role>(entity);
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

        public async Task<Role> UpsertAsync(Role entity)
        {
            await _store.UpsertAsync<Role>(entity);
            return entity;
        }
    }
}
