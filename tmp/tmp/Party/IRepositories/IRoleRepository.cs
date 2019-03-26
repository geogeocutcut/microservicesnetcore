using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.partyDomain.Model;
using Core.Common.Repository;

namespace Libragri.partyDomain.IRepositories
{
    
    public interface IRoleRepository:IRepository
    {
        Task<Role> GetByIdAsync(Guid Id);
        Task<Role> UpsertAsync(Role entity);
        Task DeleteAsync(Role entity);
        Task<IList<Role>> GetAllAsync();
        
        
        Task<IList<Role>> FindAsync(Expression<Func<Role, bool>> predicate);
    
    }
}
