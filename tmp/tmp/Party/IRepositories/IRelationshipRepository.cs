using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.partyDomain.Model;
using Core.Repository;

namespace Libragri.partyDomain.IRepositories
{
    
    public interface IRelationshipRepository:IRepository
    {
        Task<Relationship> GetByIdAsync(Guid Id);
        Task<Relationship> InsertAsync(Relationship entity);
        Task<Relationship> UpdateAsync(Relationship entity);
        Task DeleteAsync(Guid id);
        Task<IList<Relationship>> GetAllAsync();
        
        
        Task<IList<Relationship>> FindAsync(Expression<Func<Relationship, bool>> predicate);
    
    }
}
