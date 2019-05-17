using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Core.Repository;
using PartyDomain.Model;
using PartyDomain.IRepositories;
using NHibernate;
using NHibernate.Linq;
using System.Linq;

namespace PartyDomain.RepositoriesNH
{
    
    public class PurposeRepositoryNH:IPurposeRepository
    {
        private ISession _nhsession;
    	
    	public PurposeRepositoryNH(ISession nhsession)
        {
            _nhsession=nhsession;
        }
    	
    	public async Task DeleteAsync(Guid id)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                var entity = _nhsession.GetAsync<Purpose>(id);
                _nhsession.Delete(entity);
                await tx.CommitAsync();
            } 
        }

        public async Task<IList<Purpose>> FindAsync(Expression<Func<Purpose, bool>> predicate)
        {
            return await _nhsession.Query<Purpose>().Where(predicate).ToListAsync();
        }

        public async Task<IList<Purpose>> GetAllAsync()
        {
            return await _nhsession.Query<Purpose>().ToListAsync();
        }

        public async Task<Purpose> GetByIdAsync(Guid id)
        {
            return await _nhsession.GetAsync<Purpose>(id);
        }

        public async Task<Purpose> InsertAsync(Purpose entity)
        {
            Guid id = Guid.Empty;
            using (var tx = _nhsession.BeginTransaction()) {
                id = (Guid)(await _nhsession.SaveAsync(entity));
                await tx.CommitAsync();
            }
            return await GetByIdAsync(id);
        }

        public async Task<Purpose> UpdateAsync(Purpose entity)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                await _nhsession.UpdateAsync(entity);
                await tx.CommitAsync();
            }
            return await GetByIdAsync(entity.Id);
        }
    }
}
