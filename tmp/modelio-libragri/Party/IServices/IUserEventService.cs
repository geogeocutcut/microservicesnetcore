using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.PartyDomain.Model;

namespace Libragri.PartyDomain.IServices
{
    
    public interface IUserEventService
    {
        Task<UserEvent> GetByIdAsync(Guid Id);
        Task<UserEvent> AddAsync(UserEvent entity);
        Task<UserEvent> UpdateAsync(UserEvent entity);
        Task DeleteAsync(Guid Id);
        Task<IList<UserEvent>> GetAllAsync();
        
    
    }
}
