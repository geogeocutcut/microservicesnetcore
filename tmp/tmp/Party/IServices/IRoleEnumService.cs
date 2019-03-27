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
        Task<RoleEnum> UpsertAsync(RoleEnum entity);
        Task DeleteAsync(RoleEnum entity);
        Task<IList<RoleEnum>> GetAllAsync();
        
    
    }
}
