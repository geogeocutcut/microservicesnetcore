using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Libragri.partyDomain.Model;
using Libragri.partyDomain.IRepositories;
using Libragri.partyDomain.IServices;

namespace Libragri.partyDomain.Services
{
    
    public class RelationshipService:IRelationshipService
    {
        private IUnitOfWork _uow;
    	
    	public RelationshipService(IUnitOfWork uow)
    	{
    		_uow=uow;
    	}
    	
    	public async Task DeleteAsync(Guid id)
        {
            var repository = await _uow.GetRepository<IRelationshipRepository>();
            await repository.DeleteAsync(id);
        }

        public async Task<IList<Relationship>> GetAllAsync()
        {
            var repository = await _uow.GetRepository<IRelationshipRepository>();
            return await repository.GetAllAsync();
        }

        public async Task<Relationship> GetByIdAsync(Guid id)
        {
            var repository = await _uow.GetRepository<IRelationshipRepository>();
            return await repository.GetByIdAsync(id);
        }
        
        public async Task<Relationship> AddAsync(Relationship entity)
        {
        	var repository = await _uow.GetRepository<IRelationshipRepository>();
            return await repository.InsertAsync(entity);
        }
        
        public async Task<Relationship> UpdateAsync(Relationship entity)
        {
        	var repository = await _uow.GetRepository<IRelationshipRepository>();
            return await repository.UpdateAsync(entity);
        }
    }
}
