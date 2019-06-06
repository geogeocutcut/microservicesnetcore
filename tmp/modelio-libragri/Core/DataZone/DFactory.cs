using Microsoft.Practices.Unity.Configuration;
using Core.Common.Authentication;
using Core.Repository;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Unity;
using Unity.Resolution;

namespace Core.DataZone
{
    public static class DFactory
    {
        private const string SmagName = "SMAG";
        private const string DalFactoryConfigSection = "unityDALFactory";
        private const string ServiceConfigSection = "unityDServiceFactory";

        private static volatile IUnityContainer _container;
        private static volatile object _lockObj = new object();

        /// <summary>
        /// prepare and init a singleton container at the very beginning
        /// </summary>
        static DFactory()
        {
            if (_container == null)
                lock (_lockObj)
                    if (_container == null)
                        _container = new UnityContainer()
                            .LoadConfiguration((UnityConfigurationSection)ConfigurationManager.GetSection(DalFactoryConfigSection))
                            .LoadConfiguration((UnityConfigurationSection)ConfigurationManager.GetSection(ServiceConfigSection));
        }

        public static IUnitOfWork GetUnitOfWork(AuthenticationContext authCtxt = null)
        {
            if (authCtxt == null)
                return _container.Resolve<IUnitOfWork>();

            string resolveInjectionName = null;
            var dependencyOverrides = new DependencyOverrides();

            //by default, if no identity is given, we will get the default SMAG unit of work
            if (authCtxt.Identity == null)
            {
                resolveInjectionName = SmagName;
                dependencyOverrides.Add(typeof(AuthenticationContext), authCtxt);
            }
            else if (_container.IsRegistered<IUnitOfWork>(authCtxt.Identity.ApplicationType))
            {
                resolveInjectionName = authCtxt.Identity.ApplicationType;
                dependencyOverrides.Add(typeof(AuthenticationContext), authCtxt);
            }
            else if (_container.IsRegistered<IUnitOfWork>(SmagName))
            {
                resolveInjectionName = SmagName;
            }

            return _container.Resolve<IUnitOfWork>(resolveInjectionName, dependencyOverrides);
        }

        public static TUnitOfWork GetUnitOfWork<TUnitOfWork>() => _container.Resolve<TUnitOfWork>();

        public static IDServiceProvider GetServicesProvider() => _container.Resolve<IDServiceProvider>();

        public static IService GetService<IService>(IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;

            var authCtxt = new AuthenticationContext(claimsIdentity);

            return GetService<IService>(authCtxt);
        }

        public static IService GetService<IService>(AuthenticationContext authCtxt)
            => GetServicesProvider().GetService<IService>(GetUnitOfWork(authCtxt));
    }
}