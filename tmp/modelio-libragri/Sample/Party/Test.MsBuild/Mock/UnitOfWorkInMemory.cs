using System;
using System.Threading.Tasks;
using Core.Common;
using Core.Repository;
using PartyDomain.IRepositories;
using NHibernate;
using Microsoft.Extensions.DependencyInjection;
using Unity;
using System.Reflection;
using Unity.RegistrationByConvention;
using Unity.Resolution;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping;
using System.Data.SQLite;
using NHibernate.Tool.hbm2ddl;
using PartyDomain.RepositoriesNH;
using PartyDomain.Model;

namespace PartyDomain.Test.Mock
{
    public class UnitOfWorkInMemory : IUnitOfWork
    {
        const string CONNECTION_STRING = "Data Source=:memory:;BinaryGuid=False";
        private ISession _nhsession;
        private ISession _NhSession
        {
            get
            {
                if (_nhsession == null)
                {
                        _nhsession = _sessionFactory
                        .WithOptions()
                        .Interceptor(new SQLDebugOutput())
                        .Connection(GetConnection()).OpenSession();
                    
                }
                return _nhsession;
            }
        }
        private static readonly object _lockObj = new object();
        private static IUnityContainer _container;
        private static NHibernate.Cfg.Configuration _config;
        private static ISessionFactory _sessionFactory;
        private SQLiteConnection _connection;

        private int _openedTransactionCount;

        private bool _rollback;

        private string _initialDataFilename;

        static UnitOfWorkInMemory()
        {
            if (_container == null)
                lock (_lockObj)
                    if (_container == null)
                    {
                        InitializeContainer();
                        InitializeSessionFactory();
                    }
        }
        public UnitOfWorkInMemory ()//string initialDataFilename)
        {
            //_initialDataFilename=initialDataFilename;
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
            _container.RegisterType<IAddressRepository,AddressRepositoryNH>();
            _container.RegisterType<ICountryRepository,CountryRepositoryNH>();
            _container.RegisterType<IPurposeRepository,PurposeRepositoryNH>();
                  
        }
        private static void InitializeSessionFactory()
        {
            _config = new NHibernate.Cfg.Configuration()
                .DataBaseIntegration(db =>
                {
                    db.Dialect<SQLiteDialect>();
                    db.Driver<SQLite20Driver>();
                    db.ConnectionString = CONNECTION_STRING;
                    db.LogFormattedSql = true;
                    db.LogSqlInConsole = true;
                })
                .AddAssembly(typeof(UnitOfWorkNH).Assembly);
            try{
            _config.Properties["show_sql"]="true";
            }
            catch(Exception ex)
            {
                throw;
            }
                
            foreach (PersistentClass pc in _config.ClassMappings)
            {

                if (pc.Table.Name.Contains("."))
                    pc.Table.Name = pc.Table.Name.Split('.')[1];

                pc.Table.Name = pc.Table.Name.Replace("[", "").Replace("]", "");

            }


            _sessionFactory = _config.BuildSessionFactory();
        }

        private SQLiteConnection GetConnection()
        {
            if (null == _connection)
                BuildConnection();
            return _connection;
        }

        private void BuildConnection()
        {
            _connection = new SQLiteConnection(CONNECTION_STRING);
            _connection.Open();
            BuildSchema();
            if (!string.IsNullOrEmpty(_initialDataFilename))
                new SQLiteDataLoader(GetConnection()).ImportFromDbFile(_initialDataFilename);
        }

        private void BuildSchema()
        {
            SchemaExport se= new SchemaExport(_config);
            se.Execute(false, true, false, _connection, null);
            SQLiteCommand cmd = new SQLiteCommand(_connection);
            cmd.CommandText = string.Format("PRAGMA foreign_keys = ON");
            cmd.ExecuteNonQuery();
        }

        public void LoadDataFromCSV(string fileCSV)
        {
            new SQLiteDataLoader(GetConnection()).ImportFromFileCSV(fileCSV);
        }

        public TRepository GetRepository<TRepository>()
        {
            return _container.Resolve<TRepository>
            (
                new DependencyOverride(typeof(ISession), _NhSession)
            );
        }

        public void ClearCache()
        {
            _sessionFactory.EvictQueries();
            foreach (var collectionMetadata in _sessionFactory.GetAllCollectionMetadata())
                    _sessionFactory.EvictCollection(collectionMetadata.Key);
            foreach (var classMetadata in _sessionFactory.GetAllClassMetadata())
                    _sessionFactory.EvictEntity(classMetadata.Key);
        }

        public void ClearEntityCache(object ent)
        {
            _NhSession.Evict(ent);
        }
    }
}
