using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.PartyDomain.Model;
using Core.Repository;

namespace Libragri.PartyDomain.IRepositories
{
    
    public interface IUserDataRepository:IRepository
    {
        Task<UserData> GetByIdAsync(Guid Id);
        Task<UserData> InsertAsync(UserData entity);
        Task<UserData> UpdateAsync(UserData entity);
        Task DeleteAsync(Guid id);
        Task<IList<UserData>> GetAllAsync();
        
        
        Task<IList<UserData>> FindAsync(Expression<Func<UserData, bool>> predicate);
    
    }
}
