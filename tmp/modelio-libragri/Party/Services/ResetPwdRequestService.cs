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
    
    public class ResetPwdRequestService:IResetPwdRequestService
    {
        private IPartyUnitOfWork _uow;
    	
    	public ResetPwdRequestService(IPartyUnitOfWork uow)
    	{
    		_uow=uow;
    	}
    	
    	public async Task DeleteAsync(Guid id)
        {
            var repository =  _uow.GetRepository<IResetPwdRequestRepository>();
            await repository.DeleteAsync(id);
        }

        public async Task<IList<ResetPwdRequest>> GetAllAsync()
        {
            var repository =  _uow.GetRepository<IResetPwdRequestRepository>();
            return await repository.GetAllAsync();
        }

        public async Task<ResetPwdRequest> GetByIdAsync(Guid id)
        {
            var repository =  _uow.GetRepository<IResetPwdRequestRepository>();
            return await repository.GetByIdAsync(id);
        }
        
        public async Task<ResetPwdRequest> AddAsync(ResetPwdRequest entity)
        {
        	var repository =  _uow.GetRepository<IResetPwdRequestRepository>();
            return await repository.InsertAsync(entity);
        }
        
        public async Task<ResetPwdRequest> UpdateAsync(ResetPwdRequest entity)
        {
        	var repository =  _uow.GetRepository<IResetPwdRequestRepository>();
            return await repository.UpdateAsync(entity);
        }
    }
}
