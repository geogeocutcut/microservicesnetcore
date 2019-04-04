using Core.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Libragri.PartyDomain.Webapi
{
    public class Factory : IFactory
    {
        private IServiceCollection _services;

        public Factory(IServiceCollection services)
        {
            _services=services;
        }

        public bool IsRegistered<TIObject>()
        {
            throw new System.NotImplementedException();
        }

        public void Register<TIObject, TObject>()
        {
            _services.AddScoped(typeof(TIObject),typeof(TObject));
        }

        public void Register<TIObject>(TIObject obj)
        {
            _services.AddSingleton(typeof(TIObject),obj);
        }

        public TIObject Resolve<TIObject>()
        {
            return _services.BuildServiceProvider().GetService<TIObject>();
        }
    }
}