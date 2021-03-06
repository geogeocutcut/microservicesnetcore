using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.PartyDomain.Model;
using Core.Repository;

namespace Libragri.PartyDomain.IRepositories
{
    
    public interface IPartyRepository:IRepository
    {
        Task<Party> GetByIdAsync(Guid Id);
        Task<Party> InsertAsync(Party entity);
        Task<Party> UpdateAsync(Party entity);
        Task DeleteAsync(Guid id);
        Task<IList<Party>> GetAllAsync();
        
        
        Task<IList<Party>> FindAsync(Expression<Func<Party, bool>> predicate);
    
    }
}
