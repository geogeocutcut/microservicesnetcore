using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.partyDomain.Model;

namespace Libragri.partyDomain.IServices
{
    
    public interface IPartyRoleService
    {
        Task<PartyRole> GetByIdAsync(Guid Id);
        Task<PartyRole> AddAsync(PartyRole entity);
        Task<PartyRole> UpdateAsync(PartyRole entity);
        Task DeleteAsync(Guid Id);
        Task<IList<PartyRole>> GetAllAsync();
        
    
    }
}
