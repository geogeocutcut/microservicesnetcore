using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Smag.PartyDomain.Model;
using Smag.Core.Repository;

namespace Smag.PartyDomain.IRepositories
{
    
    public interface IAddressRepository:IRepository
    {
        Task<Address> GetByIdAsync(Guid Id);
        Task<Address> InsertAsync(Address entity);
        Task<Address> UpdateAsync(Address entity);
        Task DeleteAsync(Guid id);
        Task<IList<Address>> GetAllAsync();
        
        
        Task<IList<Address>> FindAsync(Expression<Func<Address, bool>> predicate);
    
    }
}
