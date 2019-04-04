using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.partyDomain.Model;

namespace Libragri.partyDomain.IServices
{
    
    public interface IRelationshipService
    {
        Task<Relationship> GetByIdAsync(Guid Id);
        Task<Relationship> AddAsync(Relationship entity);
        Task<Relationship> UpdateAsync(Relationship entity);
        Task DeleteAsync(Guid Id);
        Task<IList<Relationship>> GetAllAsync();
        
    
    }
}
