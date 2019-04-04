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
    
    public class PartyService:IPartyService
    {
        private IUnitOfWork _uow;
    	
    	public PartyService(IUnitOfWork uow)
    	{
    		_uow=uow;
    	}
    	
    	public async Task DeleteAsync(Guid id)
        {
            var repository = await _uow.GetRepository<IPartyRepository>();
            await repository.DeleteAsync(id);
        }

        public async Task<IList<Party>> GetAllAsync()
        {
            var repository = await _uow.GetRepository<IPartyRepository>();
            return await repository.GetAllAsync();
        }

        public async Task<Party> GetByIdAsync(Guid id)
        {
            var repository = await _uow.GetRepository<IPartyRepository>();
            return await repository.GetByIdAsync(id);
        }
        
        public async Task<Party> AddAsync(Party entity)
        {
        	var repository = await _uow.GetRepository<IPartyRepository>();
            return await repository.InsertAsync(entity);
        }
        
        public async Task<Party> UpdateAsync(Party entity)
        {
        	var repository = await _uow.GetRepository<IPartyRepository>();
            return await repository.UpdateAsync(entity);
        }
    }
}
