using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.partyDomain.Model;

namespace Libragri.partyDomain.IServices
{
    
    public interface IUserService
    {
        Task<User> GetByIdAsync(Guid Id);
        Task<User> UpsertAsync(User entity);
        Task DeleteAsync(User entity);
        Task<IList<User>> GetAllAsync();
        
    
    }
}
