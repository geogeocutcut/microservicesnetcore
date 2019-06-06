using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.AuthenticationDomain.Model;
using Libragri.AuthenticationDomain.IRepositories;
using Libragri.AuthenticationDomain.IServices;

namespace Libragri.AuthenticationDomain.Services
{
    
    public class UserService:IUserService
    {
        private IAuthenticationUnitOfWork _uow;
    	
    	public UserService(IAuthenticationUnitOfWork uow)
    	{
    		_uow=uow;
    	}
    	
    	public async Task DeleteAsync(Guid id)
        {
            var repository =  _uow.GetRepository<IUserRepository>();
            await repository.DeleteAsync(id);
        }

        public async Task<IList<User>> GetAllAsync()
        {
            var repository =  _uow.GetRepository<IUserRepository>();
            return await repository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var repository =  _uow.GetRepository<IUserRepository>();
            return await repository.GetByIdAsync(id);
        }
        
        public async Task<User> AddAsync(User entity)
        {
        	var repository =  _uow.GetRepository<IUserRepository>();
            return await repository.InsertAsync(entity);
        }
        
        public async Task<User> UpdateAsync(User entity)
        {
        	var repository =  _uow.GetRepository<IUserRepository>();
            return await repository.UpdateAsync(entity);
        }
    }
}
