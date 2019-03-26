using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.partyDomain.Model;
using Core.Common.Repository;

namespace Libragri.partyDomain.IRepositories
{
    
    public interface IRoleEnumRepository:IRepository
    {
        Task<RoleEnum> GetByIdAsync(Guid Id);
        Task<RoleEnum> UpsertAsync(RoleEnum entity);
        Task DeleteAsync(RoleEnum entity);
        Task<IList<RoleEnum>> GetAllAsync();
        
        
        Task<IList<RoleEnum>> FindAsync(Expression<Func<RoleEnum, bool>> predicate);
    
    }
}
