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
    
    public class UserEventService:IUserEventService
    {
        private IAuthenticationUnitOfWork _uow;
    	
    	public UserEventService(IAuthenticationUnitOfWork uow)
    	{
    		_uow=uow;
    	}
    	
    	public async Task DeleteAsync(Guid id)
        {
            var repository =  _uow.GetRepository<IUserEventRepository>();
            await repository.DeleteAsync(id);
        }

        public async Task<IList<UserEvent>> GetAllAsync()
        {
            var repository =  _uow.GetRepository<IUserEventRepository>();
            return await repository.GetAllAsync();
        }

        public async Task<UserEvent> GetByIdAsync(Guid id)
        {
            var repository =  _uow.GetRepository<IUserEventRepository>();
            return await repository.GetByIdAsync(id);
        }
        
        public async Task<UserEvent> AddAsync(UserEvent entity)
        {
        	var repository =  _uow.GetRepository<IUserEventRepository>();
            return await repository.InsertAsync(entity);
        }
        
        public async Task<UserEvent> UpdateAsync(UserEvent entity)
        {
        	var repository =  _uow.GetRepository<IUserEventRepository>();
            return await repository.UpdateAsync(entity);
        }
    }
}
