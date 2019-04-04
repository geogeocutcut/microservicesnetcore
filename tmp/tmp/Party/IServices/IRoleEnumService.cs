using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.partyDomain.Model;

namespace Libragri.partyDomain.IServices
{
    
    public interface IRoleEnumService
    {
        Task<RoleEnum> GetByIdAsync(Guid Id);
        Task<RoleEnum> AddAsync(RoleEnum entity);
        Task<RoleEnum> UpdateAsync(RoleEnum entity);
        Task DeleteAsync(Guid Id);
        Task<IList<RoleEnum>> GetAllAsync();
        
    
    }
}
