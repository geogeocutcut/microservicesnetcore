using System;
using System.Threading.Tasks;
using Core.Common;
using Core.Repository;
using Libragri.AuthenticationDomain.IRepositories;
using MongoDB.Driver;
using Unity;
using Unity.RegistrationByConvention;
using Unity.Resolution;

namespace Libragri.AuthenticationDomain.RepositoriesMongodb
{
   public class AuthenticationUnitOfWorkMongodb : IAuthenticationUnitOfWork
    {
        private readonly IMongoDatabase _db;
        
        private static readonly object _lockObj = new object();
        private static IUnityContainer _container;

        static AuthenticationUnitOfWorkMongodb()
        {
            if (_container == null)
                lock (_lockObj)
                    if (_container == null)
                    {
                        InitializeContainer();
                    }
        }

        public AuthenticationUnitOfWorkMongodb(IMongoDatabase db)
        {
            _db=db;
        }
        private static void InitializeContainer()
        {
            _container = new UnityContainer();
            
            _container.RegisterType<IUserRepository,UserRepositoryMongodb>();
            _container.RegisterType<IUserActivationRequestRepository,UserActivationRequestRepositoryMongodb>();
            _container.RegisterType<IUserRefreshTokenRepository,UserRefreshTokenRepositoryMongodb>();
            _container.RegisterType<IResetPwdRequestRepository,ResetPwdRequestRepositoryMongodb>();
            _container.RegisterType<IUserEventRepository,UserEventRepositoryMongodb>();

                  
        }
        
        
        public TRepository GetRepository<TRepository>()
        {
            return _container.Resolve<TRepository>
            (
                new DependencyOverride(typeof(IMongoDatabase), _db)
            );
        }

        public ITransaction Begin()
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}
