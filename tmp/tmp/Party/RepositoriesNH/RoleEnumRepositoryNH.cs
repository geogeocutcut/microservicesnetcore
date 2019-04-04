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
    
    public class RoleEnumRepositoryNH:IRoleEnumRepository
    {
        private ISession _nhsession;
    	
    	public RoleEnumRepositoryNH(ISession nhsession)
        {
            _nhsession=nhsession;
        }
    	
    	public async Task DeleteAsync(Guid id)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                var entity = _nhsession.GetAsync<RoleEnum>(id);
                _nhsession.Delete(entity);
                await tx.CommitAsync();
            } 
        }

        public async Task<IList<RoleEnum>> FindAsync(Expression<Func<RoleEnum, bool>> predicate)
        {
            return await _nhsession.Query<RoleEnum>().Where(predicate).ToListAsync();
        }

        public async Task<IList<RoleEnum>> GetAllAsync()
        {
            return await _nhsession.Query<RoleEnum>().ToListAsync();
        }

        public async Task<RoleEnum> GetByIdAsync(Guid id)
        {
            return await _nhsession.GetAsync<RoleEnum>(id);
        }

        public async Task<RoleEnum> InsertAsync(RoleEnum entity)
        {
            Guid id = Guid.Empty;
            using (var tx = _nhsession.BeginTransaction()) {
                id = (Guid)(await _nhsession.SaveAsync(entity));
                await tx.CommitAsync();
            }
            return await GetByIdAsync(id);
        }

        public async Task<RoleEnum> UpdateAsync(RoleEnum entity)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                await _nhsession.UpdateAsync(entity);
                await tx.CommitAsync();
            }
            return await GetByIdAsync(entity.Id);
        }
    }
}
