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
    
    public class ProfileService:IProfileService
    {
        private IAuthenticationUnitOfWork _uow;
    	
    	public ProfileService(IAuthenticationUnitOfWork uow)
    	{
    		_uow=uow;
    	}
    	
    	public async Task DeleteAsync(Guid id)
        {
            var repository =  _uow.GetRepository<IProfileRepository>();
            await repository.DeleteAsync(id);
        }

        public async Task<IList<Profile>> GetAllAsync()
        {
            var repository =  _uow.GetRepository<IProfileRepository>();
            return await repository.GetAllAsync();
        }

        public async Task<Profile> GetByIdAsync(Guid id)
        {
            var repository =  _uow.GetRepository<IProfileRepository>();
            return await repository.GetByIdAsync(id);
        }
        
        public async Task<Profile> AddAsync(Profile entity)
        {
        	var repository =  _uow.GetRepository<IProfileRepository>();
            return await repository.InsertAsync(entity);
        }
        
        public async Task<Profile> UpdateAsync(Profile entity)
        {
        	var repository =  _uow.GetRepository<IProfileRepository>();
            return await repository.UpdateAsync(entity);
        }
    }
}
