using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using PartyDomain.Model;

namespace PartyDomain.IServices
{
    
    public interface IAddressService
    {
        Task<Address> GetByIdAsync(Guid Id);
        Task<Address> AddAsync(Address entity);
        Task<Address> UpdateAsync(Address entity);
        Task DeleteAsync(Guid Id);
        Task<IList<Address>> GetAllAsync();
        
    
    }
}
