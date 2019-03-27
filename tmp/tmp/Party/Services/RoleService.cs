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
    
    public class RoleService:IRoleService
    {
        private IUnitOfWork _uow;
    	
    	public RoleService(IUnitOfWork uow)
    	{
    		_uow=uow;
    	}
    	
    	public async Task DeleteAsync(Role entity)
        {
            var repository = await _uow.GetRepository<IRoleRepository>();
            await repository.DeleteAsync(entity);
        }

        public async Task<IList<Role>> GetAllAsync()
        {
            var repository = await _uow.GetRepository<IRoleRepository>();
            return await repository.GetAllAsync();
        }

        public async Task<Role> GetByIdAsync(Guid id)
        {
            var repository = await _uow.GetRepository<IRoleRepository>();
            return await repository.GetByIdAsync(id);
        }
        
        public async Task<Role> UpsertAsync(Role entity)
        {
        	var repository = await _uow.GetRepository<IRoleRepository>();
            return await repository.UpsertAsync(entity);
        }
    }
}
