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
    
    public class UserRepository:IUserRepository
    {
        private IStore<Guid> _store;
    	
    	public UserRepository(IStore<Guid> store)
    	{
    		_store=store;
    	}
    	
    	public async Task DeleteAsync(User entity)
        {
            await _store.RemoveAsync<User>(entity);
        }

        public async Task<IList<User>> GetAllAsync()
        {
            return await _store.GetAllAsync<User>();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _store.GetByIdAsync<User>(id);
        }
        
        public async Task<IList<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            return await _store.FindAsync<User>(predicate);
        }

        public async Task<User> UpsertAsync(User entity)
        {
            await _store.UpsertAsync<User>(entity);
            return entity;
        }
    }
}
