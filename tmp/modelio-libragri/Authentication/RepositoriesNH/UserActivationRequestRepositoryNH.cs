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
    
    public class UserActivationRequestRepositoryNH:IUserActivationRequestRepository
    {
        private ISession _nhsession;
    	
    	public UserActivationRequestRepositoryNH(ISession nhsession)
        {
            _nhsession=nhsession;
        }
    	
    	public async Task DeleteAsync(Guid id)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                var entity = await _nhsession.GetAsync<UserActivationRequest>(id);
                await _nhsession.DeleteAsync(entity);
                await tx.CommitAsync();
            } 
        }

        public async Task<IList<UserActivationRequest>> FindAsync(Expression<Func<UserActivationRequest, bool>> predicate)
        {
            return await _nhsession.Query<UserActivationRequest>().Where(predicate).ToListAsync();
        }

        public async Task<IList<UserActivationRequest>> GetAllAsync()
        {
            return await _nhsession.Query<UserActivationRequest>().ToListAsync();
        }

        public async Task<UserActivationRequest> GetByIdAsync(Guid id)
        {
            return await _nhsession.GetAsync<UserActivationRequest>(id);
        }

        public async Task<UserActivationRequest> InsertAsync(UserActivationRequest entity)
        {
            Guid id = Guid.Empty;
            using (var tx = _nhsession.BeginTransaction()) {
                id = (Guid)(await _nhsession.SaveAsync(entity));
                await tx.CommitAsync();
            }
            return await GetByIdAsync(id);
        }

        public async Task<UserActivationRequest> UpdateAsync(UserActivationRequest entity)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                await _nhsession.UpdateAsync(entity);
                await tx.CommitAsync();
            }
            return await GetByIdAsync(entity.Id);
        }
    }
}
