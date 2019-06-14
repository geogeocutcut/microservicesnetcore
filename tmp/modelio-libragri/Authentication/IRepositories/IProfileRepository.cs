using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.AuthenticationDomain.Model;
using Core.Repository;

namespace Libragri.AuthenticationDomain.IRepositories
{
    
    public interface IProfileRepository:IRepository
    {
        Task<Profile> GetByIdAsync(Guid Id);
        Task<Profile> InsertAsync(Profile entity);
        Task<Profile> UpdateAsync(Profile entity);
        Task DeleteAsync(Guid id);
        Task<IList<Profile>> GetAllAsync();
        
        
        Task<IList<Profile>> FindAsync(Expression<Func<Profile, bool>> predicate);
    
    }
}
