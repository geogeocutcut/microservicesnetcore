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
    
    public class UserRefreshTokenService:IUserRefreshTokenService
    {
        private IAuthenticationUnitOfWork _uow;
    	
    	public UserRefreshTokenService(IAuthenticationUnitOfWork uow)
    	{
    		_uow=uow;
    	}
    	
    	public async Task DeleteAsync(Guid id)
        {
            var repository =  _uow.GetRepository<IUserRefreshTokenRepository>();
            await repository.DeleteAsync(id);
        }

        public async Task<IList<UserRefreshToken>> GetAllAsync()
        {
            var repository =  _uow.GetRepository<IUserRefreshTokenRepository>();
            return await repository.GetAllAsync();
        }

        public async Task<UserRefreshToken> GetByIdAsync(Guid id)
        {
            var repository =  _uow.GetRepository<IUserRefreshTokenRepository>();
            return await repository.GetByIdAsync(id);
        }
        
        public async Task<UserRefreshToken> AddAsync(UserRefreshToken entity)
        {
        	var repository =  _uow.GetRepository<IUserRefreshTokenRepository>();
            return await repository.InsertAsync(entity);
        }
        
        public async Task<UserRefreshToken> UpdateAsync(UserRefreshToken entity)
        {
        	var repository =  _uow.GetRepository<IUserRefreshTokenRepository>();
            return await repository.UpdateAsync(entity);
        }
    }
}
