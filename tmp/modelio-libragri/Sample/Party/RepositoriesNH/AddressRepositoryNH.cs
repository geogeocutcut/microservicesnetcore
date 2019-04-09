using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Smag.Core.Repository;
using Smag.PartyDomain.Model;
using Smag.PartyDomain.IRepositories;
using NHibernate;
using NHibernate.Linq;
using System.Linq;

namespace Smag.PartyDomain.RepositoriesNH
{
    
    public class AddressRepositoryNH:IAddressRepository
    {
        private ISession _nhsession;
    	
    	public AddressRepositoryNH(ISession nhsession)
        {
            _nhsession=nhsession;
        }
    	
    	public async Task DeleteAsync(Guid id)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                var entity = _nhsession.GetAsync<Address>(id);
                _nhsession.Delete(entity);
                await tx.CommitAsync();
            } 
        }

        public async Task<IList<Address>> FindAsync(Expression<Func<Address, bool>> predicate)
        {
            return await _nhsession.Query<Address>().Where(predicate).ToListAsync();
        }

        public async Task<IList<Address>> GetAllAsync()
        {
            return await _nhsession.Query<Address>().ToListAsync();
        }

        public async Task<Address> GetByIdAsync(Guid id)
        {
            return await _nhsession.GetAsync<Address>(id);
        }

        public async Task<Address> InsertAsync(Address entity)
        {
            Guid id = Guid.Empty;
            using (var tx = _nhsession.BeginTransaction()) {
                id = (Guid)(await _nhsession.SaveAsync(entity));
                await tx.CommitAsync();
            }
            return await GetByIdAsync(id);
        }

        public async Task<Address> UpdateAsync(Address entity)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                await _nhsession.UpdateAsync(entity);
                await tx.CommitAsync();
            }
            return await GetByIdAsync(entity.Id);
        }
    }
}
