using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using PartyDomain.Model;
using PartyDomain.IRepositories;
using PartyDomain.IServices;
using System.Diagnostics;
using Core.Repository;
using PartyDomain.Specification;
using Core.Common;

namespace PartyDomain.Services
{
    
    public class PartyService:IPartyService
    {
        private IUnitOfWork _uow;
    	
    	public PartyService(IUnitOfWork uow)
    	{
    		_uow=uow;
    	}
    	
    	public async Task DeleteAsync(Guid id)
        {
            var repository = _uow.GetRepository<IPartyRepository>();
            await repository.DeleteAsync(id);
        }

        public async Task<IList<Party>> GetAllAsync()
        {
            var repository = _uow.GetRepository<IPartyRepository>();
            var result =await repository.GetAllAsync();
            return result;
        }

        public async Task<Party> GetByIdAsync(Guid id)
        {
            var repository = _uow.GetRepository<IPartyRepository>();
            return await repository.GetByIdAsync(id);
        }
        
        public async Task<Party> AddAsync(Party entity)
        {
            using(var tx = _uow.Begin() )
            {
                var res = PartySpecification.IsSatisfiedBy(entity);

                if (!res.IsSuccess)
                    throw new BusinessException("Validation Error",res.Messages);

                var repository = _uow.GetRepository<IPartyRepository>();
                return await repository.InsertAsync(entity);
                
                _uow.Commit();
            }
        }
        
        public async Task<Party> UpdateAsync(Party entity)
        {
        	var repository = _uow.GetRepository<IPartyRepository>();
            return await repository.UpdateAsync(entity);
        }
    }
}
