using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.PartyDomain.Model;
using Libragri.PartyDomain.IRepositories;
using Libragri.PartyDomain.IServices;

namespace Libragri.PartyDomain.Service
{
    
    public class PartyService:IPartyService
    {
        private IUnitOfWork _uow;
    	
    	public PartyService(IUnitOfWork uow)
    	{
    		_uow=uow;
    	}
    	
    	public async Task DeleteAsync(Party entity)
        {
            var repository = await _uow.GetRepository<IPartyRepository>();
            await repository.DeleteAsync(entity);
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
        
        public async Task<Party> UpsertAsync(Party entity)
        {
        	var repository = await _uow.GetRepository<IPartyRepository>();
            return await repository.UpsertAsync(entity);
        }
    }
}
