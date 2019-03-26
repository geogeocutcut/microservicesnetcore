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
    
    public class RoleEnumRepository:IRoleEnumRepository
    {
        private IStore<Guid> _store;
    	
    	public RoleEnumRepository(IStore<Guid> store)
    	{
    		_store=store;
    	}
    	
    	public async Task DeleteAsync(RoleEnum entity)
        {
            await _store.RemoveAsync<RoleEnum>(entity);
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

        public async Task<RoleEnum> UpsertAsync(RoleEnum entity)
        {
            await _store.UpsertAsync<RoleEnum>(entity);
            return entity;
        }
    }
}
