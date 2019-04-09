using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Smag.PartyDomain.Model;
using Smag.Core.Repository;

namespace Smag.PartyDomain.IRepositories
{
    
    public interface ICountryRepository:IRepository
    {
        Task<Country> GetByIdAsync(Guid Id);
        Task<Country> InsertAsync(Country entity);
        Task<Country> UpdateAsync(Country entity);
        Task DeleteAsync(Guid id);
        Task<IList<Country>> GetAllAsync();
        
        
        Task<IList<Country>> FindAsync(Expression<Func<Country, bool>> predicate);
    
    }
}