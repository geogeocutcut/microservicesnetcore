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
    
    public class ResetPwdRequestRepositoryNH:IResetPwdRequestRepository
    {
        private ISession _nhsession;
    	
    	public ResetPwdRequestRepositoryNH(ISession nhsession)
        {
            _nhsession=nhsession;
        }
    	
    	public async Task DeleteAsync(Guid id)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                var entity = await _nhsession.GetAsync<ResetPwdRequest>(id);
                await _nhsession.DeleteAsync(entity);
                await tx.CommitAsync();
            } 
        }

        public async Task<IList<ResetPwdRequest>> FindAsync(Expression<Func<ResetPwdRequest, bool>> predicate)
        {
            return await _nhsession.Query<ResetPwdRequest>().Where(predicate).ToListAsync();
        }

        public async Task<IList<ResetPwdRequest>> GetAllAsync()
        {
            return await _nhsession.Query<ResetPwdRequest>().ToListAsync();
        }

        public async Task<ResetPwdRequest> GetByIdAsync(Guid id)
        {
            return await _nhsession.GetAsync<ResetPwdRequest>(id);
        }

        public async Task<ResetPwdRequest> InsertAsync(ResetPwdRequest entity)
        {
            Guid id = Guid.Empty;
            using (var tx = _nhsession.BeginTransaction()) {
                id = (Guid)(await _nhsession.SaveAsync(entity));
                await tx.CommitAsync();
            }
            return await GetByIdAsync(id);
        }

        public async Task<ResetPwdRequest> UpdateAsync(ResetPwdRequest entity)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                await _nhsession.UpdateAsync(entity);
                await tx.CommitAsync();
            }
            return await GetByIdAsync(entity.Id);
        }
    }
}
