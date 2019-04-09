using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Smag.PartyDomain.Model;

namespace Smag.PartyDomain.IServices
{
    
    public interface ICountryService
    {
        Task<Country> GetByIdAsync(Guid Id);
        Task<Country> AddAsync(Country entity);
        Task<Country> UpdateAsync(Country entity);
        Task DeleteAsync(Guid Id);
        Task<IList<Country>> GetAllAsync();
        
    
    }
}