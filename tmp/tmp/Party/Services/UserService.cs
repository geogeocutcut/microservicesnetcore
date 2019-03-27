using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.partyDomain.Model;
using Libragri.partyDomain.IRepositories;
using Libragri.partyDomain.IServices;

namespace Libragri.partyDomain.Services
{
    
    public class UserService:IUserService
    {
        private IUnitOfWork _uow;
    	
    	public UserService(IUnitOfWork uow)
    	{
    		_uow=uow;
    	}
    	
    	public async Task DeleteAsync(User entity)
        {
            var repository = await _uow.GetRepository<IUserRepository>();
            await repository.DeleteAsync(entity);
        }

        public async Task<IList<User>> GetAllAsync()
        {
            var repository = await _uow.GetRepository<IUserRepository>();
            return await repository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var repository = await _uow.GetRepository<IUserRepository>();
            return await repository.GetByIdAsync(id);
        }
        
        public async Task<User> UpsertAsync(User entity)
        {
        	var repository = await _uow.GetRepository<IUserRepository>();
            return await repository.UpsertAsync(entity);
        }
    }
}
