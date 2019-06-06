using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.AuthenticationDomain.Model;
using Core.Repository;

namespace Libragri.AuthenticationDomain.IRepositories
{
    
    public interface IUserRefreshTokenRepository:IRepository
    {
        Task<UserRefreshToken> GetByIdAsync(Guid Id);
        Task<UserRefreshToken> InsertAsync(UserRefreshToken entity);
        Task<UserRefreshToken> UpdateAsync(UserRefreshToken entity);
        Task DeleteAsync(Guid id);
        Task<IList<UserRefreshToken>> GetAllAsync();
        
        
        Task<IList<UserRefreshToken>> FindAsync(Expression<Func<UserRefreshToken, bool>> predicate);
    
    }
}
