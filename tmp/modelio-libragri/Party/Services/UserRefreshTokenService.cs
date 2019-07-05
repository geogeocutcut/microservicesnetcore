using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.PartyDomain.Model;
using Libragri.PartyDomain.IRepositories;
using Libragri.PartyDomain.IServices;
using System.Linq;
using Core.Common;

namespace Libragri.PartyDomain.Services
{
    
    public class UserRefreshTokenService:IUserRefreshTokenService
    {
        private IPartyUnitOfWork _uow;
    	
    	public UserRefreshTokenService(IPartyUnitOfWork uow)
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


        public async Task<UserRefreshToken> GetByRefreshToken(string refreshToken)
        {
            var repository =  _uow.GetRepository<IUserRefreshTokenRepository>();
            var token = (await repository.FindAsync(t =>t.RefreshToken==refreshToken)).FirstOrDefault();
            if(token==null)
            {
                throw new BusinessException("bad authentification","bad  token!");
            }
            return token;
        }
    }
}
