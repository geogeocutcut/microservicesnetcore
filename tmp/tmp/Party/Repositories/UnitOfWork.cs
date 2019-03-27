using System;
using System.Threading.Tasks;
using Core.Common.Factory;
using Core.Common.Repository;
using Core.Common.Store;
using Libragri.partyDomain.IRepositories;

namespace Libragri.partyDomain.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IStore<Guid> _store;
        private IFactory _factory;
        public UnitOfWork(IStore<Guid> store,IFactory factory)
        {
            _store =store;
            _factory=factory;
            
                        _factory.Register<IPartyRepository,PartyRepository>();
            _factory.Register<IUserRepository,UserRepository>();
            _factory.Register<IPartyRoleRepository,PartyRoleRepository>();
            _factory.Register<IRelationshipRepository,RelationshipRepository>();
            _factory.Register<IRoleRepository,RoleRepository>();
            _factory.Register<IRoleEnumRepository,RoleEnumRepository>();

        }
        public async Task<TRepository> GetRepository<TRepository>() where TRepository : IRepository
        {
            return _factory.Resolve<TRepository>(_store);
        }
    }
}
