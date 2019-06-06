using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.AuthenticationDomain.Model;
using Core.Repository;

namespace Libragri.AuthenticationDomain.IRepositories
{
    
    public interface IUserEventRepository:IRepository
    {
        Task<UserEvent> GetByIdAsync(Guid Id);
        Task<UserEvent> InsertAsync(UserEvent entity);
        Task<UserEvent> UpdateAsync(UserEvent entity);
        Task DeleteAsync(Guid id);
        Task<IList<UserEvent>> GetAllAsync();
        
        
        Task<IList<UserEvent>> FindAsync(Expression<Func<UserEvent, bool>> predicate);
    
    }
}
