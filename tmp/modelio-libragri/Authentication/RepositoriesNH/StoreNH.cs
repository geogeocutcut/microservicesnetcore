using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Common.Model;
using NHibernate;
using NHibernate.Linq;

namespace Libragri.AuthenticationDomain.RepositoriesNH
{
    public class StoreNH<TEntity> where TEntity : BaseEntity<Guid>
    {
        protected ISession _nhsession;

        
        public StoreNH(ISession session)
        {
            _nhsession=session;
        }
        public virtual async Task DeleteAsync(Guid id)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                var entity = await _nhsession.GetAsync<TEntity>(id);
                if(entity!=null)
                    await _nhsession.DeleteAsync(entity);
                await tx.CommitAsync();
            } 
        }

        public virtual async Task<IList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _nhsession.Query<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual async Task<IList<TEntity>> GetAllAsync()
        {
            return await _nhsession.Query<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _nhsession.GetAsync<TEntity>(id);
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            Guid id = Guid.Empty;
            using (var tx = _nhsession.BeginTransaction()) {
                id = (Guid)(await _nhsession.SaveAsync(entity));
                await tx.CommitAsync();
            }
            return await GetByIdAsync(id);
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                await _nhsession.UpdateAsync(entity);
                await tx.CommitAsync();
            }
            return await GetByIdAsync(entity.Id);
        }
    }
}