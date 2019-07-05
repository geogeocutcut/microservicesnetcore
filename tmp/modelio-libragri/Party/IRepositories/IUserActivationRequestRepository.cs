using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.PartyDomain.Model;
using Core.Repository;

namespace Libragri.PartyDomain.IRepositories
{
    
    public interface IUserActivationRequestRepository:IRepository
    {
        Task<UserActivationRequest> GetByIdAsync(Guid Id);
        Task<UserActivationRequest> InsertAsync(UserActivationRequest entity);
        Task<UserActivationRequest> UpdateAsync(UserActivationRequest entity);
        Task DeleteAsync(Guid id);
        Task<IList<UserActivationRequest>> GetAllAsync();
        
        
        Task<IList<UserActivationRequest>> FindAsync(Expression<Func<UserActivationRequest, bool>> predicate);
    
    }
}
