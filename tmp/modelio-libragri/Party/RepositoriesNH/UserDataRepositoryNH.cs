using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Core.Repository;
using Libragri.PartyDomain.Model;
using Libragri.PartyDomain.IRepositories;
using NHibernate;
using NHibernate.Linq;
using System.Linq;

namespace Libragri.PartyDomain.RepositoriesNH
{
    
    public class UserDataRepositoryNH:IUserDataRepository
    {
        private ISession _nhsession;
    	
    	public UserDataRepositoryNH(ISession nhsession)
        {
            _nhsession=nhsession;
        }
    	
    	public async Task DeleteAsync(Guid id)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                var entity = await _nhsession.GetAsync<UserData>(id);
                await _nhsession.DeleteAsync(entity);
                await tx.CommitAsync();
            } 
        }

        public async Task<IList<UserData>> FindAsync(Expression<Func<UserData, bool>> predicate)
        {
            return await _nhsession.Query<UserData>().Where(predicate).ToListAsync();
        }

        public async Task<IList<UserData>> GetAllAsync()
        {
            return await _nhsession.Query<UserData>().ToListAsync();
        }

        public async Task<UserData> GetByIdAsync(Guid id)
        {
            return await _nhsession.GetAsync<UserData>(id);
        }

        public async Task<UserData> InsertAsync(UserData entity)
        {
            Guid id = Guid.Empty;
            using (var tx = _nhsession.BeginTransaction()) {
                id = (Guid)(await _nhsession.SaveAsync(entity));
                await tx.CommitAsync();
            }
            return await GetByIdAsync(id);
        }

        public async Task<UserData> UpdateAsync(UserData entity)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                await _nhsession.UpdateAsync(entity);
                await tx.CommitAsync();
            }
            return await GetByIdAsync(entity.Id);
        }
    }
}
