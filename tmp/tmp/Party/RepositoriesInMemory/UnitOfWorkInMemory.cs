using System;
using System.Threading.Tasks;
using Core.Common;
using Core.Repository;
using Libragri.partyDomain.IRepositories;

namespace Libragri.partyDomain.RepositoriesInMemory
{
    public class UnitOfWorkInMemory : IUnitOfWork
    {
        private IStore<Guid> _store;
        private IFactory _factory;
        public UnitOfWorkInMemory(IStore<Guid> store,IFactory factory)
        {
            _store =store;
            _factory=factory;
            
            _factory.Register<IPartyRepository,PartyRepositoryInMemory>();
            _factory.Register<IUserRepository,UserRepositoryInMemory>();
            _factory.Register<IPartyRoleRepository,PartyRoleRepositoryInMemory>();
            _factory.Register<IRelationshipRepository,RelationshipRepositoryInMemory>();
            _factory.Register<IRoleRepository,RoleRepositoryInMemory>();
            _factory.Register<IRoleEnumRepository,RoleEnumRepositoryInMemory>();

        }
        public async Task<TRepository> GetRepository<TRepository>() where TRepository : IRepository
        {
            return _factory.Resolve<TRepository>();
        }
    }
}
