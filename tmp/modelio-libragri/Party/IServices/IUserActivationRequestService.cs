using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.PartyDomain.Model;

namespace Libragri.PartyDomain.IServices
{
    
    public interface IUserActivationRequestService
    {
        Task<UserActivationRequest> GetByIdAsync(Guid Id);
        Task<UserActivationRequest> AddAsync(UserActivationRequest entity);
        Task<UserActivationRequest> UpdateAsync(UserActivationRequest entity);
        Task DeleteAsync(Guid Id);
        Task<IList<UserActivationRequest>> GetAllAsync();
        
    
    }
}
