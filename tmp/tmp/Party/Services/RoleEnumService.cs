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
    
    public class RoleEnumService:IRoleEnumService
    {
        private IUnitOfWork _uow;
    	
    	public RoleEnumService(IUnitOfWork uow)
    	{
    		_uow=uow;
    	}
    	
    	public async Task DeleteAsync(Guid id)
        {
            var repository = await _uow.GetRepository<IRoleEnumRepository>();
            await repository.DeleteAsync(id);
        }

        public async Task<IList<RoleEnum>> GetAllAsync()
        {
            var repository = await _uow.GetRepository<IRoleEnumRepository>();
            return await repository.GetAllAsync();
        }

        public async Task<RoleEnum> GetByIdAsync(Guid id)
        {
            var repository = await _uow.GetRepository<IRoleEnumRepository>();
            return await repository.GetByIdAsync(id);
        }
        
        public async Task<RoleEnum> AddAsync(RoleEnum entity)
        {
        	var repository = await _uow.GetRepository<IRoleEnumRepository>();
            return await repository.InsertAsync(entity);
        }
        
        public async Task<RoleEnum> UpdateAsync(RoleEnum entity)
        {
        	var repository = await _uow.GetRepository<IRoleEnumRepository>();
            return await repository.UpdateAsync(entity);
        }
    }
}
