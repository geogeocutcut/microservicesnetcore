using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Smag.PartyDomain.Model;
using Smag.Core.Repository;

namespace Smag.PartyDomain.IRepositories
{
    
    public interface IPurposeRepository:IRepository
    {
        Task<Purpose> GetByIdAsync(Guid Id);
        Task<Purpose> InsertAsync(Purpose entity);
        Task<Purpose> UpdateAsync(Purpose entity);
        Task DeleteAsync(Guid id);
        Task<IList<Purpose>> GetAllAsync();
        
        
        Task<IList<Purpose>> FindAsync(Expression<Func<Purpose, bool>> predicate);
    
    }
}
