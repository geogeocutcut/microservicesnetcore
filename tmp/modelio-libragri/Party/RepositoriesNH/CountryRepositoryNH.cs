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
    
    public class CountryRepositoryNH:ICountryRepository
    {
        private ISession _nhsession;
    	
    	public CountryRepositoryNH(ISession nhsession)
        {
            _nhsession=nhsession;
        }
    	
    	public async Task DeleteAsync(Guid id)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                var entity = await _nhsession.GetAsync<Country>(id);
                await _nhsession.DeleteAsync(entity);
                await tx.CommitAsync();
            } 
        }

        public async Task<IList<Country>> FindAsync(Expression<Func<Country, bool>> predicate)
        {
            return await _nhsession.Query<Country>().Where(predicate).ToListAsync();
        }

        public async Task<IList<Country>> GetAllAsync()
        {
            return await _nhsession.Query<Country>().ToListAsync();
        }

        public async Task<Country> GetByIdAsync(Guid id)
        {
            return await _nhsession.GetAsync<Country>(id);
        }

        public async Task<Country> InsertAsync(Country entity)
        {
            Guid id = Guid.Empty;
            using (var tx = _nhsession.BeginTransaction()) {
                id = (Guid)(await _nhsession.SaveAsync(entity));
                await tx.CommitAsync();
            }
            return await GetByIdAsync(id);
        }

        public async Task<Country> UpdateAsync(Country entity)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                await _nhsession.UpdateAsync(entity);
                await tx.CommitAsync();
            }
            return await GetByIdAsync(entity.Id);
        }
    }
}
