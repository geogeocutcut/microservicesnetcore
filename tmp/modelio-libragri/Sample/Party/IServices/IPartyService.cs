using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using PartyDomain.Model;

namespace PartyDomain.IServices
{
    
    public interface IPartyService
    {
        Task<Party> GetByIdAsync(Guid Id);
        Task<Party> AddAsync(Party entity);
        Task<Party> UpdateAsync(Party entity);
        Task DeleteAsync(Guid Id);
        Task<IList<Party>> GetAllAsync();
        
    
    }
}
