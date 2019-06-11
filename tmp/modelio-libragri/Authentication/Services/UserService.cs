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
            var repository =  _uow.GetRepository<IUserRepository>();
            await repository.DeleteAsync(id);
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
            var pwdSHA256 = ComputeSha256Hash(pwd);
            if(user == null || user?.PwdSHA256!=pwdSHA256)
            {
                throw new BusinessException("bad authentication", "unknown user !");
            }
            return user;
        }

        private string ComputeSha256Hash(string rawData)  
        {  
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())  
            {  
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));  
  
                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();  
                for (int i = 0; i < bytes.Length; i++)  
                {  
                    builder.Append(bytes[i].ToString("x2"));  
                }  
                return builder.ToString();  
            }  
        }
    }
}
