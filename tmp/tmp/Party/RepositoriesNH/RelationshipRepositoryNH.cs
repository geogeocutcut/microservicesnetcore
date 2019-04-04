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
    
    public class RelationshipRepositoryNH:IRelationshipRepository
    {
        private ISession _nhsession;
    	
    	public RelationshipRepositoryNH(ISession nhsession)
        {
            _nhsession=nhsession;
        }
    	
    	public async Task DeleteAsync(Guid id)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                var entity = _nhsession.GetAsync<Relationship>(id);
                _nhsession.Delete(entity);
                await tx.CommitAsync();
            } 
        }

        public async Task<IList<Relationship>> FindAsync(Expression<Func<Relationship, bool>> predicate)
        {
            return await _nhsession.Query<Relationship>().Where(predicate).ToListAsync();
        }

        public async Task<IList<Relationship>> GetAllAsync()
        {
            return await _nhsession.Query<Relationship>().ToListAsync();
        }

        public async Task<Relationship> GetByIdAsync(Guid id)
        {
            return await _nhsession.GetAsync<Relationship>(id);
        }

        public async Task<Relationship> InsertAsync(Relationship entity)
        {
            Guid id = Guid.Empty;
            using (var tx = _nhsession.BeginTransaction()) {
                id = (Guid)(await _nhsession.SaveAsync(entity));
                await tx.CommitAsync();
            }
            return await GetByIdAsync(id);
        }

        public async Task<Relationship> UpdateAsync(Relationship entity)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                await _nhsession.UpdateAsync(entity);
                await tx.CommitAsync();
            }
            return await GetByIdAsync(entity.Id);
        }
    }
}
