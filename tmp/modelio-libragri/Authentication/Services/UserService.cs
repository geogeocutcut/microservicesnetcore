using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.AuthenticationDomain.Model;
using Libragri.AuthenticationDomain.IRepositories;
using Libragri.AuthenticationDomain.IServices;
using System.Security.Cryptography;
using Core.Common;
using System.Linq;
using Common.Crypto;

namespace Libragri.AuthenticationDomain.Services
{
    
    public class UserService:IUserService
    {
        private IAuthenticationUnitOfWork _uow;
    	
    	public UserService(IAuthenticationUnitOfWork uow)
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
            var userRepo =  _uow.GetRepository<IUserRepository>();


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

            // using(var t = _uow.Begin())
            // {
            //     var rshTokenRepo =_uow.GetRepository<IUserRefreshTokenRepository>();
            //     var rshTokensTask = rshTokenRepo.FindAsync(x =>x.User.Id==id);
            //     var activationRequestRepo =_uow.GetRepository<IUserActivationRequestRepository>();
            //     var activationRequestsTask = activationRequestRepo.FindAsync(x =>x.User.Id==id);
            //     var userEventRepo =_uow.GetRepository<IUserEventRepository>();
            //     var userEventsTask = userEventRepo.FindAsync(x =>x.User.Id==id);
            //     var rstPwdRequestRepo =_uow.GetRepository<IUserEventRepository>();
            //     var rstPwdRequestsTask = rstPwdRequestRepo.FindAsync(x =>x.User.Id==id);
            //     var userRepo =  _uow.GetRepository<IUserRepository>();

            //     var beforeDeleteTasks = new List<Task>();
            //     var rshTokens = rshTokensTask.Result;
            //     foreach(var tok in rshTokens)
            //     {
            //         beforeDeleteTasks.Add(rshTokenRepo.DeleteAsync(tok.Id));
            //     }
            //     var activationRequests = activationRequestsTask.Result;
            //     foreach(var req in activationRequests)
            //     {
            //         beforeDeleteTasks.Add(activationRequestRepo.DeleteAsync(req.Id));
            //     }
            //     var userEvents = userEventsTask.Result;
            //     foreach(var evt in userEvents)
            //     {
            //         beforeDeleteTasks.Add(userEventRepo.DeleteAsync(evt.Id));
            //     }
            //     var rstPwdRequests = rstPwdRequestsTask.Result;
            //     foreach(var req in rstPwdRequests)
            //     {
            //         beforeDeleteTasks.Add(rstPwdRequestRepo.DeleteAsync(req.Id));
            //     }

            //     Task.WaitAll(beforeDeleteTasks.ToArray());

            //     await userRepo.DeleteAsync(id);
            //     t.Commit();
                
            // }
        }

        public async Task<IList<User>> GetAllAsync()
        {
            var repository =  _uow.GetRepository<IUserRepository>();
            return await repository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var repository =  _uow.GetRepository<IUserRepository>();
            return await repository.GetByIdAsync(id);
        }
        
        public async Task<User> AddAsync(User entity)
        {
            entity.PwdSHA256=CryptoUtilities.ComputeSha256Hash(entity.PwdSHA256);
            //entity.Profiles= new HashSet<Profile>();
        	var repository =  _uow.GetRepository<IUserRepository>();
            return await repository.InsertAsync(entity);
        }
        
        public async Task<User> UpdateAsync(User entity)
        {
        	var repository =  _uow.GetRepository<IUserRepository>();
            return await repository.UpdateAsync(entity);
        }

        public async Task<User> GetByLoginPassword(string login, string pwd)
        {
            var repository =  _uow.GetRepository<IUserRepository>(); 
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
