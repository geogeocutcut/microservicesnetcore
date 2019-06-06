using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.AuthenticationDomain.Model;

namespace Libragri.AuthenticationDomain.IServices
{
    
    public interface IResetPwdRequestService
    {
        Task<ResetPwdRequest> GetByIdAsync(Guid Id);
        Task<ResetPwdRequest> AddAsync(ResetPwdRequest entity);
        Task<ResetPwdRequest> UpdateAsync(ResetPwdRequest entity);
        Task DeleteAsync(Guid Id);
        Task<IList<ResetPwdRequest>> GetAllAsync();
        
    
    }
}
