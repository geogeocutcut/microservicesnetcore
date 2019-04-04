using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.partyDomain.Model;

namespace Libragri.partyDomain.IServices
{
    
    public interface IRoleService
    {
        Task<Role> GetByIdAsync(Guid Id);
        Task<Role> AddAsync(Role entity);
        Task<Role> UpdateAsync(Role entity);
        Task DeleteAsync(Guid Id);
        Task<IList<Role>> GetAllAsync();
        
    
    }
}
