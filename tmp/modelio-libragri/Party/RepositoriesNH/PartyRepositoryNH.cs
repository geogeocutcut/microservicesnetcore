using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Core.Repository;
using Smag.PartyDomain.Model;
using Smag.PartyDomain.IRepositories;
using NHibernate;
using NHibernate.Linq;
using System.Linq;

namespace Smag.PartyDomain.RepositoriesNH
{
    
    public class PartyRepositoryNH:IPartyRepository
    {
        private ISession _nhsession;
    	
    	public PartyRepositoryNH(ISession nhsession)
        {
            _nhsession=nhsession;
        }
    	
    	public async Task DeleteAsync(Guid id)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                var entity = await _nhsession.GetAsync<Party>(id);
                await _nhsession.DeleteAsync(entity);
                await tx.CommitAsync();
            } 
        }

        public async Task<IList<Party>> FindAsync(Expression<Func<Party, bool>> predicate)
        {
            return await _nhsession.Query<Party>().Where(predicate).ToListAsync();
        }

        public async Task<IList<Party>> GetAllAsync()
        {
            return await _nhsession.Query<Party>()
            .FetchMany(x=>x.Addresses)
            .ThenFetch(x=>x.country)
            .FetchMany(x=>x.Addresses)
            .ThenFetch(x=>x.purposes)
            .FetchMany(x=>x.partyRole)
            .ToListAsync();
        }

        public async Task<Party> GetByIdAsync(Guid id)
        {
            return await _nhsession.Query<Party>()
            .FetchMany(x=>x.Addresses)
            .ThenFetch(x=>x.country)
            .FetchMany(x=>x.Addresses)
            .ThenFetch(x=>x.purposes)
            .FetchMany(x=>x.partyRole)
            .FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Party> InsertAsync(Party entity)
        {
            Guid id = Guid.Empty;
            using (var tx = _nhsession.BeginTransaction()) {
                id = (Guid)(await _nhsession.SaveAsync(entity));
                await tx.CommitAsync();
            }
            return await GetByIdAsync(id);
        }

        public async Task<Party> UpdateAsync(Party entity)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                await _nhsession.UpdateAsync(entity);
                await tx.CommitAsync();
            }
            return await GetByIdAsync(entity.Id);
        }
    }
}
