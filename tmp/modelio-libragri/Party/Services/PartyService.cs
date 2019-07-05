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
    
    public class PartyService:IPartyService
    {
        private IPartyUnitOfWork _uow;
    	
    	public PartyService(IPartyUnitOfWork uow)
    	{
    		_uow=uow;
    	}
    	
    	public async Task DeleteAsync(Guid id)
        {
            var repository =  _uow.GetRepository<IPartyRepository>();
            await repository.DeleteAsync(id);
        }

        public async Task<IList<Party>> GetAllAsync()
        {
            var repository =  _uow.GetRepository<IPartyRepository>();
            return await repository.GetAllAsync();
        }

        public async Task<Party> GetByIdAsync(Guid id)
        {
            var repository =  _uow.GetRepository<IPartyRepository>();
            return await repository.GetByIdAsync(id);
        }
        
        public async Task<Party> AddAsync(Party entity)
        {
        	var repository =  _uow.GetRepository<IPartyRepository>();
            return await repository.InsertAsync(entity);
        }
        
        public async Task<Party> UpdateAsync(Party entity)
        {
        	var repository =  _uow.GetRepository<IPartyRepository>();
            return await repository.UpdateAsync(entity);
        }
    }
}
