using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.AuthenticationDomain.Model;

namespace Libragri.AuthenticationDomain.IServices
{
    
    public interface IProfileService
    {
        Task<Profile> GetByIdAsync(Guid Id);
        Task<Profile> AddAsync(Profile entity);
        Task<Profile> UpdateAsync(Profile entity);
        Task DeleteAsync(Guid Id);
        Task<IList<Profile>> GetAllAsync();
        
    
    }
}
