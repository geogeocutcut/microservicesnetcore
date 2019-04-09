using Smag.Core.Repository;

namespace Smag.Core.DataZone
{
    public interface IDServiceProvider
    {
        IService GetService<IService>(IUnitOfWork uow);
    }
}