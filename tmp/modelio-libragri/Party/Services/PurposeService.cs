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
    
    public class PurposeService:IPurposeService
    {
        private IPartyUnitOfWork _uow;
    	
    	public PurposeService(IPartyUnitOfWork uow)
    	{
    		_uow=uow;
    	}
    	
    	public async Task DeleteAsync(Guid id)
        {
            var repository =  _uow.GetRepository<IPurposeRepository>();
            await repository.DeleteAsync(id);
        }

        public async Task<IList<Purpose>> GetAllAsync()
        {
            var repository =  _uow.GetRepository<IPurposeRepository>();
            return await repository.GetAllAsync();
        }

        public async Task<Purpose> GetByIdAsync(Guid id)
        {
            var repository =  _uow.GetRepository<IPurposeRepository>();
            return await repository.GetByIdAsync(id);
        }
        
        public async Task<Purpose> AddAsync(Purpose entity)
        {
        	var repository =  _uow.GetRepository<IPurposeRepository>();
            return await repository.InsertAsync(entity);
        }
        
        public async Task<Purpose> UpdateAsync(Purpose entity)
        {
        	var repository =  _uow.GetRepository<IPurposeRepository>();
            return await repository.UpdateAsync(entity);
        }
    }
}
