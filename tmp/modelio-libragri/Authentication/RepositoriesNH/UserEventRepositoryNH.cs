using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Core.Repository;
using Libragri.AuthenticationDomain.Model;
using Libragri.AuthenticationDomain.IRepositories;
using NHibernate;
using NHibernate.Linq;
using System.Linq;

namespace Libragri.AuthenticationDomain.RepositoriesNH
{
    
    public class UserEventRepositoryNH:IUserEventRepository
    {
        private ISession _nhsession;
    	
    	public UserEventRepositoryNH(ISession nhsession)
        {
            _nhsession=nhsession;
        }
    	
    	public async Task DeleteAsync(Guid id)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                var entity = await _nhsession.GetAsync<UserEvent>(id);
                await _nhsession.DeleteAsync(entity);
                await tx.CommitAsync();
            } 
        }

        public async Task<IList<UserEvent>> FindAsync(Expression<Func<UserEvent, bool>> predicate)
        {
            return await _nhsession.Query<UserEvent>().Where(predicate).ToListAsync();
        }

        public async Task<IList<UserEvent>> GetAllAsync()
        {
            return await _nhsession.Query<UserEvent>().ToListAsync();
        }

        public async Task<UserEvent> GetByIdAsync(Guid id)
        {
            return await _nhsession.GetAsync<UserEvent>(id);
        }

        public async Task<UserEvent> InsertAsync(UserEvent entity)
        {
            Guid id = Guid.Empty;
            using (var tx = _nhsession.BeginTransaction()) {
                id = (Guid)(await _nhsession.SaveAsync(entity));
                await tx.CommitAsync();
            }
            return await GetByIdAsync(id);
        }

        public async Task<UserEvent> UpdateAsync(UserEvent entity)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                await _nhsession.UpdateAsync(entity);
                await tx.CommitAsync();
            }
            return await GetByIdAsync(entity.Id);
        }
    }
}
