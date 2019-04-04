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
    
    public class PartyRoleService:IPartyRoleService
    {
        private IUnitOfWork _uow;
    	
    	public PartyRoleService(IUnitOfWork uow)
    	{
    		_uow=uow;
    	}
    	
    	public async Task DeleteAsync(Guid id)
        {
            var repository = await _uow.GetRepository<IPartyRoleRepository>();
            await repository.DeleteAsync(id);
        }

        public async Task<IList<PartyRole>> GetAllAsync()
        {
            var repository = await _uow.GetRepository<IPartyRoleRepository>();
            return await repository.GetAllAsync();
        }

        public async Task<PartyRole> GetByIdAsync(Guid id)
        {
            var repository = await _uow.GetRepository<IPartyRoleRepository>();
            return await repository.GetByIdAsync(id);
        }
        
        public async Task<PartyRole> AddAsync(PartyRole entity)
        {
        	var repository = await _uow.GetRepository<IPartyRoleRepository>();
            return await repository.InsertAsync(entity);
        }
        
        public async Task<PartyRole> UpdateAsync(PartyRole entity)
        {
        	var repository = await _uow.GetRepository<IPartyRoleRepository>();
            return await repository.UpdateAsync(entity);
        }
    }
}
