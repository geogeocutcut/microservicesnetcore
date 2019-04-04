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
    
    public class PartyRoleRepositoryNH:IPartyRoleRepository
    {
        private ISession _nhsession;
    	
    	public PartyRoleRepositoryNH(ISession nhsession)
        {
            _nhsession=nhsession;
        }
    	
    	public async Task DeleteAsync(Guid id)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                var entity = _nhsession.GetAsync<PartyRole>(id);
                _nhsession.Delete(entity);
                await tx.CommitAsync();
            } 
        }

        public async Task<IList<PartyRole>> FindAsync(Expression<Func<PartyRole, bool>> predicate)
        {
            return await _nhsession.Query<PartyRole>().Where(predicate).ToListAsync();
        }

        public async Task<IList<PartyRole>> GetAllAsync()
        {
            return await _nhsession.Query<PartyRole>().ToListAsync();
        }

        public async Task<PartyRole> GetByIdAsync(Guid id)
        {
            return await _nhsession.GetAsync<PartyRole>(id);
        }

        public async Task<PartyRole> InsertAsync(PartyRole entity)
        {
            Guid id = Guid.Empty;
            using (var tx = _nhsession.BeginTransaction()) {
                id = (Guid)(await _nhsession.SaveAsync(entity));
                await tx.CommitAsync();
            }
            return await GetByIdAsync(id);
        }

        public async Task<PartyRole> UpdateAsync(PartyRole entity)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                await _nhsession.UpdateAsync(entity);
                await tx.CommitAsync();
            }
            return await GetByIdAsync(entity.Id);
        }
    }
}
