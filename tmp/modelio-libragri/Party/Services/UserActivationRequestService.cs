using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.PartyDomain.Model;
using Libragri.PartyDomain.IRepositories;
using Libragri.PartyDomain.IServices;

namespace Libragri.PartyDomain.Services
{
    
    public class UserActivationRequestService:IUserActivationRequestService
    {
        private IPartyUnitOfWork _uow;
    	
    	public UserActivationRequestService(IPartyUnitOfWork uow)
    	{
    		_uow=uow;
    	}
    	
    	public async Task DeleteAsync(Guid id)
        {
            var repository =  _uow.GetRepository<IUserActivationRequestRepository>();
            await repository.DeleteAsync(id);
        }

        public async Task<IList<UserActivationRequest>> GetAllAsync()
        {
            var repository =  _uow.GetRepository<IUserActivationRequestRepository>();
            return await repository.GetAllAsync();
        }

        public async Task<UserActivationRequest> GetByIdAsync(Guid id)
        {
            var repository =  _uow.GetRepository<IUserActivationRequestRepository>();
            return await repository.GetByIdAsync(id);
        }
        
        public async Task<UserActivationRequest> AddAsync(UserActivationRequest entity)
        {
        	var repository =  _uow.GetRepository<IUserActivationRequestRepository>();
            return await repository.InsertAsync(entity);
        }
        
        public async Task<UserActivationRequest> UpdateAsync(UserActivationRequest entity)
        {
        	var repository =  _uow.GetRepository<IUserActivationRequestRepository>();
            return await repository.UpdateAsync(entity);
        }
    }
}
