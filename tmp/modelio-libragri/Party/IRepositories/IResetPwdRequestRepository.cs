using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.PartyDomain.Model;
using Core.Repository;

namespace Libragri.PartyDomain.IRepositories
{
    
    public interface IResetPwdRequestRepository:IRepository
    {
        Task<ResetPwdRequest> GetByIdAsync(Guid Id);
        Task<ResetPwdRequest> InsertAsync(ResetPwdRequest entity);
        Task<ResetPwdRequest> UpdateAsync(ResetPwdRequest entity);
        Task DeleteAsync(Guid id);
        Task<IList<ResetPwdRequest>> GetAllAsync();
        
        
        Task<IList<ResetPwdRequest>> FindAsync(Expression<Func<ResetPwdRequest, bool>> predicate);
    
    }
}
