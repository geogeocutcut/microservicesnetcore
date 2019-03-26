using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.partyDomain.Model;
using Core.Common.Repository;

namespace Libragri.partyDomain.IRepositories
{
    
    public interface IPartyRoleRepository:IRepository
    {
        Task<PartyRole> GetByIdAsync(Guid Id);
        Task<PartyRole> UpsertAsync(PartyRole entity);
        Task DeleteAsync(PartyRole entity);
        Task<IList<PartyRole>> GetAllAsync();
        
        
        Task<IList<PartyRole>> FindAsync(Expression<Func<PartyRole, bool>> predicate);
    
    }
}
