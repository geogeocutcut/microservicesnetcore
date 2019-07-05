using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.PartyDomain.Model;
using Libragri.PartyDomain.IRepositories;
using Libragri.PartyDomain.IServices;
using System.Linq;
using Common.Crypto;
using Core.Common;

namespace Libragri.PartyDomain.Services
{
    
    public class UserDataService:IUserDataService
    {
        private IPartyUnitOfWork _uow;
    	
    	public UserDataService(IPartyUnitOfWork uow)
    	{
    		_uow=uow;
    	}
    	
    	public async Task DeleteAsync(Guid id)
        {
            var rshTokenRepo =_uow.GetRepository<IUserRefreshTokenRepository>();
            var rshTokens = await rshTokenRepo.FindAsync(x =>x.User.Id==id);
            var activationRequestRepo =_uow.GetRepository<IUserActivationRequestRepository>();
            var activationRequests = await activationRequestRepo.FindAsync(x =>x.User.Id==id);
            var userEventRepo =_uow.GetRepository<IUserEventRepository>();
            var userEvents = await userEventRepo.FindAsync(x =>x.User.Id==id);
            var rstPwdRequestRepo =_uow.GetRepository<IUserEventRepository>();
            var rstPwdRequests = await rstPwdRequestRepo.FindAsync(x =>x.User.Id==id);
            var userRepo =  _uow.GetRepository<IUserDataRepository>();


            using(var t = _uow.Begin())
            {
                foreach(var tok in rshTokens)
                {
                    await rshTokenRepo.DeleteAsync(tok.Id);
                }
                foreach(var req in activationRequests)
                {
                    await activationRequestRepo.DeleteAsync(req.Id);
                }
                foreach(var evt in userEvents)
                {
                    await userEventRepo.DeleteAsync(evt.Id);
                }
                foreach(var req in rstPwdRequests)
                {
                    await rstPwdRequestRepo.DeleteAsync(req.Id);
                }

                await userRepo.DeleteAsync(id);
                t.Commit();
                
            }
        }

        public async Task<IList<UserData>> GetAllAsync()
        {
            var repository =  _uow.GetRepository<IUserDataRepository>();
            return await repository.GetAllAsync();
        }

        public async Task<UserData> GetByIdAsync(Guid id)
        {
            var repository =  _uow.GetRepository<IUserDataRepository>();
            return await repository.GetByIdAsync(id);
        }
        
        public async Task<UserData> AddAsync(UserData entity)
        {
        	var repository =  _uow.GetRepository<IUserDataRepository>();
            return await repository.InsertAsync(entity);
        }
        
        public async Task<UserData> UpdateAsync(UserData entity)
        {
        	var repository =  _uow.GetRepository<IUserDataRepository>();
            return await repository.UpdateAsync(entity);
        }
        
        public async Task<UserData> GetByLoginPassword(string login, string pwd)
        {
            var repository =  _uow.GetRepository<IUserDataRepository>(); 
            var user = (await repository.FindAsync(u=>u.Login==login)).FirstOrDefault();
            var pwdSHA256 = CryptoUtilities.ComputeSha256Hash(pwd);
            if(user == null || user?.PwdSHA256!=pwdSHA256)
            {
                throw new BusinessException("bad authentication", "unknown user !");
            }
            return user;
        }
    }
}
