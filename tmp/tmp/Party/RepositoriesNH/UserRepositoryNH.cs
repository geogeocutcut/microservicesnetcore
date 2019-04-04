using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Core.Repository;
using Libragri.partyDomain.Model;
using Libragri.partyDomain.IRepositories;
using NHibernate;
using NHibernate.Linq;
using System.Linq;

namespace Libragri.partyDomain.RepositoriesNH
{
    
    public class UserRepositoryNH:IUserRepository
    {
        private ISession _nhsession;
    	
    	public UserRepositoryNH(ISession nhsession)
        {
            _nhsession=nhsession;
        }
    	
    	public async Task DeleteAsync(Guid id)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                var entity = _nhsession.GetAsync<User>(id);
                _nhsession.Delete(entity);
                await tx.CommitAsync();
            } 
        }

        public async Task<IList<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            return await _nhsession.Query<User>().Where(predicate).ToListAsync();
        }

        public async Task<IList<User>> GetAllAsync()
        {
            return await _nhsession.Query<User>().ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _nhsession.GetAsync<User>(id);
        }

        public async Task<User> InsertAsync(User entity)
        {
            Guid id = Guid.Empty;
            using (var tx = _nhsession.BeginTransaction()) {
                id = (Guid)(await _nhsession.SaveAsync(entity));
                await tx.CommitAsync();
            }
            return await GetByIdAsync(id);
        }

        public async Task<User> UpdateAsync(User entity)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                await _nhsession.UpdateAsync(entity);
                await tx.CommitAsync();
            }
            return await GetByIdAsync(entity.Id);
        }
    }
}
