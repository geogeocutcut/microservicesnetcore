using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.AuthenticationDomain.Model;

namespace Libragri.AuthenticationDomain.IServices
{
    
    public interface IUserService
    {
        Task<User> GetByIdAsync(Guid Id);
        Task<User> AddAsync(User entity);
        Task<User> UpdateAsync(User entity);
        Task DeleteAsync(Guid Id);
        Task<IList<User>> GetAllAsync();
        
    
    }
}
