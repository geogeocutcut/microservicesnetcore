using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.PartyDomain.Model;

namespace Libragri.PartyDomain.IServices
{
    
    public interface IUserRefreshTokenService
    {
        Task<UserRefreshToken> GetByIdAsync(Guid Id);
        Task<UserRefreshToken> AddAsync(UserRefreshToken entity);
        Task<UserRefreshToken> UpdateAsync(UserRefreshToken entity);
        Task DeleteAsync(Guid Id);
        Task<IList<UserRefreshToken>> GetAllAsync();
        
        Task<UserRefreshToken> GetByRefreshToken(string refreshToken);
        
    
    }
}
