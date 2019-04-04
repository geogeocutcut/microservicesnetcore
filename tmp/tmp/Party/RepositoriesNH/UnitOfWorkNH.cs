using System;
using System.Threading.Tasks;
using Core.Common;
using Core.Repository;
using Libragri.partyDomain.IRepositories;
using NHibernate;

namespace Libragri.partyDomain.RepositoriesNH
{
   public class UnitOfWorkNH : IUnitOfWork
    {
        private ISession _session;
        private IFactory _factory;
        public UnitOfWorkNH(ISession session,IFactory factory)
        {
            _session =session;
            _factory=factory;
            
            _factory.Register<IPartyRepository,PartyRepositoryNH>();
            _factory.Register<IUserRepository,UserRepositoryNH>();
            _factory.Register<IPartyRoleRepository,PartyRoleRepositoryNH>();
            _factory.Register<IRelationshipRepository,RelationshipRepositoryNH>();
            _factory.Register<IRoleRepository,RoleRepositoryNH>();
            _factory.Register<IRoleEnumRepository,RoleEnumRepositoryNH>();


        }
        public async Task<TRepository> GetRepository<TRepository>() where TRepository : IRepository
        {
            return _factory.Resolve<TRepository>();
        }
    }
}
