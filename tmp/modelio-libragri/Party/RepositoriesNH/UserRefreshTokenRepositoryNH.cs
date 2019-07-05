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
    
    public class UserRefreshTokenRepositoryNH:IUserRefreshTokenRepository
    {
        private ISession _nhsession;
    	
    	public UserRefreshTokenRepositoryNH(ISession nhsession)
        {
            _nhsession=nhsession;
        }
    	
    	public async Task DeleteAsync(Guid id)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                var entity = await _nhsession.GetAsync<UserRefreshToken>(id);
                await _nhsession.DeleteAsync(entity);
                await tx.CommitAsync();
            } 
        }

        public async Task<IList<UserRefreshToken>> FindAsync(Expression<Func<UserRefreshToken, bool>> predicate)
        {
            return await _nhsession.Query<UserRefreshToken>().Where(predicate).ToListAsync();
        }

        public async Task<IList<UserRefreshToken>> GetAllAsync()
        {
            return await _nhsession.Query<UserRefreshToken>().ToListAsync();
        }

        public async Task<UserRefreshToken> GetByIdAsync(Guid id)
        {
            return await _nhsession.GetAsync<UserRefreshToken>(id);
        }

        public async Task<UserRefreshToken> InsertAsync(UserRefreshToken entity)
        {
            Guid id = Guid.Empty;
            using (var tx = _nhsession.BeginTransaction()) {
                id = (Guid)(await _nhsession.SaveAsync(entity));
                await tx.CommitAsync();
            }
            return await GetByIdAsync(id);
        }

        public async Task<UserRefreshToken> UpdateAsync(UserRefreshToken entity)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                await _nhsession.UpdateAsync(entity);
                await tx.CommitAsync();
            }
            return await GetByIdAsync(entity.Id);
        }
    }
}
