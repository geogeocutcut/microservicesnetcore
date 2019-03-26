
using System;
using System.Threading.Tasks;
using Core.Common.Factory;
using Core.Common.Repository;
using Core.Common.Store;
using Libragri.PartyDomain.IRepositories;

namespace Libragri.PartyDomain.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IStore<Guid> _store;
        private IFactory _factory;
        public UnitOfWork(IStore<Guid> store,IFactory factory)
        {
            _store =store;
            _factory=factory;
        }
        public async Task<TRepository> GetRepository<TRepository>() where TRepository : IRepository
        {
            return _factory.Resolve<TRepository>(_store);
        }
    }
}