using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.partyDomain.Model;
using Core.Repository;

namespace Libragri.partyDomain.IRepositories
{
    
    public interface IRoleEnumRepository:IRepository
    {
        Task<RoleEnum> GetByIdAsync(Guid Id);
        Task<RoleEnum> InsertAsync(RoleEnum entity);
        Task<RoleEnum> UpdateAsync(RoleEnum entity);
        Task DeleteAsync(Guid id);
        Task<IList<RoleEnum>> GetAllAsync();
        
        
        Task<IList<RoleEnum>> FindAsync(Expression<Func<RoleEnum, bool>> predicate);
    
    }
}
