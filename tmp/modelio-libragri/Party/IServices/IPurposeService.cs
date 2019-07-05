using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.PartyDomain.Model;

namespace Libragri.PartyDomain.IServices
{
    
    public interface IPurposeService
    {
        Task<Purpose> GetByIdAsync(Guid Id);
        Task<Purpose> AddAsync(Purpose entity);
        Task<Purpose> UpdateAsync(Purpose entity);
        Task DeleteAsync(Guid Id);
        Task<IList<Purpose>> GetAllAsync();
        
    
    }
}
