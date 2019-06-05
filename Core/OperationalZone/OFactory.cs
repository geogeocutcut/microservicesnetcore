using Microsoft.Practices.Unity.Configuration;
using Core.Common.Authentication;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Unity;
using Unity.Interception.Utilities;
using Unity.Resolution;

namespace Core.OperationalZone
{
    public static class OFactory
    {
        private const string UnityOProxyFactorySectionName = "unityOProxyFactory";
        private const string UnityOServiceFactorySectionName = "unityOServiceFactory";

        private static volatile IUnityContainer _container;
        private static volatile object _syncRoot = new object();

        public static IOProxyProvider GetProxyProvider(IDictionary<Type, object> dependencyOverrides = null)
        {
            if (_container == null)
                lock (_syncRoot)
                    if (_container == null)
                        InitializeContainer();

            var dependencyOverridesDico = new DependencyOverrides();

            if (dependencyOverrides != null && dependencyOverrides.Any())
                dependencyOverrides.ForEach(x => dependencyOverridesDico.Add(x.Key, x.Value));

            return _container.Resolve<IOProxyProvider>(dependencyOverridesDico);
        }

        private static void InitializeContainer()
            => _container = new UnityContainer()
            .LoadConfiguration((UnityConfigurationSection)ConfigurationManager.GetSection(UnityOServiceFactorySectionName))
            .LoadConfiguration((UnityConfigurationSection)ConfigurationManager.GetSection(UnityOProxyFactorySectionName));

        public static IOServiceProvider GetServicesProvider()
        {
            if (_container == null)
                lock (_syncRoot)
                    if (_container == null)
                        InitializeContainer();

            return _container.Resolve<IOServiceProvider>();
        }

        public static IService GetService<IService>(IIdentity identity)
        {
            var authContext = (identity == null || !(identity is ClaimsIdentity claimsIdentity))
                ? new AuthenticationContext()
                : new AuthenticationContext(claimsIdentity);

            var provider = GetProxyProvider(new Dictionary<Type, object>
            {
                {
                    typeof(AuthenticationContext),
                    authContext
                }
            });

            return GetServicesProvider().GetService<IService>(provider);
        }

        public static IService GetService<IService>(AuthenticationContext authCtxt)
        {
            var provider = GetProxyProvider(new Dictionary<Type, object>
            {
                {
                    typeof(AuthenticationContext),
                    authCtxt
                }
            });

            return GetServicesProvider().GetService<IService>(provider);
        }

        public static IService GetService<IService>(IOProxyProvider provider)
            => GetServicesProvider().GetService<IService>(provider);
    }
}