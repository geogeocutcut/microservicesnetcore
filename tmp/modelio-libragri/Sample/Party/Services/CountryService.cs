using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using PartyDomain.Model;
using PartyDomain.IRepositories;
using PartyDomain.IServices;
using Core.Repository;

namespace PartyDomain.Services
{
    
    public class CountryService:ICountryService
    {
        private IUnitOfWork _uow;
    	
    	public CountryService(IUnitOfWork uow)
    	{
    		_uow=uow;
    	}
    	
    	public async Task DeleteAsync(Guid id)
        {
            var repository = _uow.GetRepository<ICountryRepository>();
            await repository.DeleteAsync(id);
        }

        public async Task<IList<Country>> GetAllAsync()
        {
            var repository = _uow.GetRepository<ICountryRepository>();
            return await repository.GetAllAsync();
        }

        public async Task<Country> GetByIdAsync(Guid id)
        {
            var repository = _uow.GetRepository<ICountryRepository>();
            return await repository.GetByIdAsync(id);
        }
        
        public async Task<Country> AddAsync(Country entity)
        {
        	var repository = _uow.GetRepository<ICountryRepository>();
            return await repository.InsertAsync(entity);
        }
        
        public async Task<Country> UpdateAsync(Country entity)
        {
        	var repository = _uow.GetRepository<ICountryRepository>();
            return await repository.UpdateAsync(entity);
        }
    }
}
