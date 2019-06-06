using System;
using System.Threading.Tasks;
using Core.Common;
using Core.Repository;
using Smag.PartyDomain.IRepositories;
using NHibernate;
using Unity;
using Unity.RegistrationByConvention;
using Unity.Resolution;

namespace Smag.PartyDomain.RepositoriesNH
{
   public class PartyUnitOfWorkNH : IPartyUnitOfWork
    {
        private ISession _nhsession;
        private ISession _NhSession
        {
            get
            {
                if (_nhsession == null)
                {
                        _nhsession = _sessionFactory.OpenSession();
                    
                }
                return _nhsession;
            }
        }
        private static readonly object _lockObj = new object();
        private static IUnityContainer _container;
        private static ISessionFactory _sessionFactory;

        static PartyUnitOfWorkNH()
        {
            if (_container == null)
                lock (_lockObj)
                    if (_container == null)
                    {
                        InitializeContainer();
                        InitializeSessionFactory();
                    }
        }

        private int _openedTransactionCount;

        private bool _rollback;

        private static void InitializeSessionFactory()
        {
            var cfg = new NHibernate.Cfg.Configuration();
            cfg.Configure("hibernate.cfg.xml");
            _sessionFactory= cfg.BuildSessionFactory();
        }

        public Core.Repository.ITransaction Begin()
        {
            if (!_NhSession.Transaction.IsActive)
            {
                _NhSession.BeginTransaction();
                _rollback = false;
                _openedTransactionCount = 1;
            }
            else
            {
                _openedTransactionCount += 1;
            }
            return new Transaction(this);
        }

        public void Commit()
        {
            if (_openedTransactionCount > 1)
            {
                _openedTransactionCount -= 1;
            }
            else if (_rollback)
            {
                if (!_NhSession.Transaction.IsActive)
                {
                    _NhSession.Transaction.Rollback();
                }
            }
            else
            {
                if (_NhSession.Transaction.IsActive)
                {
                    try
                    {
                        _NhSession.Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        _NhSession.Transaction.Rollback();
                    }
                }

                if (_NhSession.Transaction.IsActive)
                    _NhSession.Transaction.Rollback();
            }
        }

        public void Dispose()
        {
            if (_NhSession.Transaction.IsActive)
                _NhSession.Transaction.Rollback();
        }


        public void Rollback()
        {
            throw new NotImplementedException();
        }

        private static void InitializeContainer()
        {
            _container = new UnityContainer();
            
            _container.RegisterType<IPartyRepository,PartyRepositoryNH>();
            _container.RegisterType<ICountryRepository,CountryRepositoryNH>();
            _container.RegisterType<IPurposeRepository,PurposeRepositoryNH>();

                  
        }
        
        
        public TRepository GetRepository<TRepository>()
        {
            return _container.Resolve<TRepository>
            (
                new DependencyOverride(typeof(ISession), _NhSession)
            );
        }
    }
}
