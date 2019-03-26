using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.partyDomain.Model;
using Core.Common.Repository;

namespace Libragri.partyDomain.IRepositories
{
    
    public interface IUserRepository:IRepository
    {
        Task<User> GetByIdAsync(Guid Id);
        Task<User> UpsertAsync(User entity);
        Task DeleteAsync(User entity);
        Task<IList<User>> GetAllAsync();
        
        
        Task<IList<User>> FindAsync(Expression<Func<User, bool>> predicate);
    
    }
}
