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
    
    public class UserRepositoryInMemory:IUserRepository
    {
        private IStore<Guid> _store;
    	
    	public UserRepositoryInMemory(IStore<Guid> store)
    	{
    		_store=store;
    	}
    	
    	public async Task DeleteAsync(Guid id)
        {
            await _store.RemoveAsync<User>(id);
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

        public async Task<User> InsertAsync(User entity)
        {
            await _store.UpsertAsync<User>(entity);
            return entity;
        }
        public async Task<User> UpdateAsync(User entity)
        {
            await _store.UpsertAsync<User>(entity);
            return entity;
        }
    }
}
