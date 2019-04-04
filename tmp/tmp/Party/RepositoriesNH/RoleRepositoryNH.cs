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
    
    public class RoleRepositoryNH:IRoleRepository
    {
        private ISession _nhsession;
    	
    	public RoleRepositoryNH(ISession nhsession)
        {
            _nhsession=nhsession;
        }
    	
    	public async Task DeleteAsync(Guid id)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                var entity = _nhsession.GetAsync<Role>(id);
                _nhsession.Delete(entity);
                await tx.CommitAsync();
            } 
        }

        public async Task<IList<Role>> FindAsync(Expression<Func<Role, bool>> predicate)
        {
            return await _nhsession.Query<Role>().Where(predicate).ToListAsync();
        }

        public async Task<IList<Role>> GetAllAsync()
        {
            return await _nhsession.Query<Role>().ToListAsync();
        }

        public async Task<Role> GetByIdAsync(Guid id)
        {
            return await _nhsession.GetAsync<Role>(id);
        }

        public async Task<Role> InsertAsync(Role entity)
        {
            Guid id = Guid.Empty;
            using (var tx = _nhsession.BeginTransaction()) {
                id = (Guid)(await _nhsession.SaveAsync(entity));
                await tx.CommitAsync();
            }
            return await GetByIdAsync(id);
        }

        public async Task<Role> UpdateAsync(Role entity)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                await _nhsession.UpdateAsync(entity);
                await tx.CommitAsync();
            }
            return await GetByIdAsync(entity.Id);
        }
    }
}
