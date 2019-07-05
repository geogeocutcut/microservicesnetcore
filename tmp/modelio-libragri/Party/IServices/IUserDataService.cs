using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.PartyDomain.Model;

namespace Libragri.PartyDomain.IServices
{
    
    public interface IUserDataService
    {
        Task<UserData> GetByIdAsync(Guid Id);
        Task<UserData> AddAsync(UserData entity);
        Task<UserData> UpdateAsync(UserData entity);
        Task DeleteAsync(Guid Id);
        Task<IList<UserData>> GetAllAsync();
        

        Task<UserData> GetByLoginPassword(string login,string pwd);
    }
}
