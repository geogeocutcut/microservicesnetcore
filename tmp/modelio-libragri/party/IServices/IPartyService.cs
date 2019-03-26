using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.PartyDomain.Model;

namespace Libragri.PartyDomain.IServices
{
    
    public interface IPartyService
    {
        Task<Party> GetByIdAsync(Guid Id);
        Task<Party> UpsertAsync(Party entity);
        Task DeleteAsync(Party entity);
        Task<IList<Party>> GetAllAsync();
    }    
}
