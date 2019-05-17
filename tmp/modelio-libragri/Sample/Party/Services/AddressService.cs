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
    
    public class AddressService:IAddressService
    {
        private IUnitOfWork _uow;
    	
    	public AddressService(IUnitOfWork uow)
    	{
    		_uow=uow;
    	}
    	
    	public async Task DeleteAsync(Guid id)
        {
            var repository = _uow.GetRepository<IAddressRepository>();
            await repository.DeleteAsync(id);
        }

        public async Task<IList<Address>> GetAllAsync()
        {
            var repository = _uow.GetRepository<IAddressRepository>();
            return await repository.GetAllAsync();
        }

        public async Task<Address> GetByIdAsync(Guid id)
        {
            var repository = _uow.GetRepository<IAddressRepository>();
            return await repository.GetByIdAsync(id);
        }
        
        public async Task<Address> AddAsync(Address entity)
        {
        	var repository = _uow.GetRepository<IAddressRepository>();
            return await repository.InsertAsync(entity);
        }
        
        public async Task<Address> UpdateAsync(Address entity)
        {
        	var repository = _uow.GetRepository<IAddressRepository>();
            return await repository.UpdateAsync(entity);
        }
    }
}
